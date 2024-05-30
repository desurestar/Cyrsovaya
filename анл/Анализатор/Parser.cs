using System.CodeDom;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms.Design.Behavior;
using static Анализатор.TkType;

namespace Анализатор
{
    internal class Parser
    {
        public static string res = null;
        public static string matr = null;
        private Stack<Token> tokens;
        private Token currentToken;

        public Parser(List<Token> tokens)
        {
            this.tokens = new Stack<Token>(tokens.Reverse<Token>());
            currentToken = this.tokens.Count > 0 ? this.tokens.Pop() : null;

            res = null;
            matr = null;
        }

        private void NextToken()
        {
            currentToken = tokens.Count > 0 ? tokens.Pop() : null;
        }

        public void Program()
        {
            StatementList();

            if (currentToken != null && currentToken.Type != TokenType.EMPTY)
            {
                throw new Exception("Ошибочный токен в конце программы");
            }
        }

        private void StatementList()
        {
            while (currentToken != null && (currentToken.Type == TokenType.DIM ||
                   currentToken.Type == TokenType.IDENTIFIER ||
                   currentToken.Type == TokenType.SELECT))
            {
                Statement();
            }
        }

        private void Statement()
        {
            if (currentToken == null) return; // Добавлено для предотвращения null reference

            switch (currentToken.Type)
            {
                case TokenType.DIM:
                    NextToken();
                    VariableList();
                    Expect(TokenType.AS);
                    DataType();
                    Expect(TokenType.NEW_LINE);
                    break;

                case TokenType.IDENTIFIER:
                    NextToken();
                    Expect(TokenType.EQUAL);
                    Expression();
                    Expect(TokenType.NEW_LINE);
                    break;

                case TokenType.SELECT:
                    NextToken();
                    Expect(TokenType.CASE);
                    Expect(TokenType.IDENTIFIER);
                    Expect(TokenType.NEW_LINE);
                    Body();
                    Expect(TokenType.END);
                    Expect(TokenType.SELECT);
                    Expect(TokenType.NEW_LINE);
                    break;

                default:
                    throw new Exception("Неожиданный токен в операторе: " + currentToken);
            }
        }

        private void VariableList()
        {
            Expect(TokenType.IDENTIFIER);
            while (currentToken != null && currentToken.Type == TokenType.COMMA)
            {
                NextToken();
                Expect(TokenType.IDENTIFIER);
            }
        }

        private void Body()
        {
            while (currentToken != null && currentToken.Type == TokenType.CASE)
            {
                Case();
            }
        }

        private void Case()
        {
            Expect(TokenType.CASE);
            CaseCondition();
            StatementList();
        }

        private void CaseCondition()
        {
            if (currentToken == null) return; // Добавлено для предотвращения null reference

            switch (currentToken.Type)
            {
                case TokenType.LITERAL:
                    NextToken();
                    if (currentToken != null && currentToken.Type == TokenType.TO)
                    {
                        NextToken();
                        Expect(TokenType.LITERAL);
                    }
                    Expect(TokenType.NEW_LINE);
                    break;

                case TokenType.ELSE:
                    NextToken();
                    Expect(TokenType.NEW_LINE);
                    break;

                default:
                    throw new Exception("Неожиданный токен в состоянии case: " + currentToken);
            }
        }

        private void DataType()
        {
            if (currentToken == null) return; // Добавлено для предотвращения null reference

            switch (currentToken.Type)
            {
                case TokenType.INTEGER:
                case TokenType.BOOL:
                case TokenType.STRING:
                    NextToken();
                    break;

                default:
                    throw new Exception("Неожиданный тип данных: " + currentToken);
            }
        }

        private void Expression()
        {
            var outputQueue = new Queue<Token>();
            var operatorStack = new Stack<Token>();
            var matrix = new List<string>();
            int matrixIndex = 1;

            while (currentToken != null && currentToken.Type != TokenType.NEW_LINE && currentToken.Type != TokenType.EMPTY)
            {
                if (IsOperand(currentToken.Type))
                {
                    outputQueue.Enqueue(currentToken);
                }
                else if (IsOperator(currentToken.Type))
                {
                    while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek().Type) &&
                           GetPrecedence(operatorStack.Peek().Type) >= GetPrecedence(currentToken.Type))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(currentToken);
                }
                else if (currentToken.Type == TokenType.LPAR)
                {
                    operatorStack.Push(currentToken);
                }
                else if (currentToken.Type == TokenType.RPAR)
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek().Type != TokenType.LPAR)
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    if (operatorStack.Count == 0 || operatorStack.Peek().Type != TokenType.LPAR)
                    {
                        throw new Exception("Не найдена соответствующая левая круглая скобка.");
                    }

                    operatorStack.Pop();
                }
                else
                {
                    throw new Exception($"Неожиданный токен в выражении: {currentToken}");
                }
                NextToken();
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek().Type == TokenType.LPAR)
                {
                    throw new Exception("Не найдена соответствующая правая круглая скобка.");
                }
                outputQueue.Enqueue(operatorStack.Pop());
            }

            // Формируем обратную польскую запись
            StringBuilder rpnOutput = new StringBuilder();
            foreach (var token in outputQueue)
            {
                rpnOutput.Append($"{token.Value} ");
            }
            string rpnResult = rpnOutput.ToString().Trim();
            res += rpnResult + "\r\n";

            // Формируем матрицу выражений
            ValidateAndBuildMatrix(outputQueue, ref matrix, ref matrixIndex);

            foreach (var line in matrix)
            {
                matr += line + "\r\n";
            }
            matr += "\r\n";
        }

        private bool IsOperand(TokenType type)
        {
            return type == TokenType.LITERAL || type == TokenType.IDENTIFIER;
        }

        private bool IsOperator(TokenType type)
        {
            return type == TokenType.PLUS || type == TokenType.MINUS || type == TokenType.MULTIPLY || type == TokenType.DIVISION;
        }

        private int GetPrecedence(TokenType type)
        {
            switch (type)
            {
                case TokenType.MULTIPLY:
                case TokenType.DIVISION:
                    return 3;
                case TokenType.PLUS:
                case TokenType.MINUS:
                    return 2;
                default:
                    return 1;
            }
        }

        private void ValidateAndBuildMatrix(Queue<Token> rpn, ref List<string> matrix, ref int matrixIndex)
        {
            Stack<string> tempStack = new Stack<string>();

            foreach (var token in rpn)
            {
                if (IsOperand(token.Type))
                {
                    tempStack.Push(token.Value);
                }
                else if (IsOperator(token.Type))
                {
                    if (tempStack.Count < 2)
                    {
                        throw new Exception("Недопустимое выражение: недостаточно операндов для оператора");
                    }
                    string operand2 = tempStack.Pop();
                    string operand1 = tempStack.Pop();

                    // Проверка деления на ноль
                    if (token.Type == TokenType.DIVISION && operand2 == "0" || operand2 == "0\r")
                    {
                        throw new Exception("Ошибка: Деление на ноль");
                    }

                    string matrixEntry = $"M{matrixIndex++}: {token.Value} {operand1} {operand2}";
                    tempStack.Push($"M{matrixIndex - 1}");
                    matrix.Add(matrixEntry);
                }
                else
                {
                    throw new Exception($"Недопустимый токен в выражении: {token}");
                }
            }

            if (tempStack.Count != 1)
            {
                throw new Exception("Недопустимое выражение: некорректные операнды и операторы");
            }
        }

        private void Expect(TokenType expected)
        {
            if (currentToken == null)
            {
                if (expected == TokenType.NEW_LINE && tokens.Count == 0)
                {
                    return;
                }
                throw new Exception($"Ожидается {expected}, а на входе конец токенов.");
            }
            if (currentToken.Type != expected)
            {
                throw new Exception($"Ожидается {expected}, а на входе {currentToken.Type} с индексом токена {currentToken.index}");
            }
            NextToken();
        }

    }
}