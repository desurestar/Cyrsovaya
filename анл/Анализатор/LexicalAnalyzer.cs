using static Анализатор.TkType;


namespace Анализатор
{
    internal class LexicalAnalyzer
    {
        

        public static bool IsDelimiter(Token token)
        {
            return Delimiters.Contains(token.Type);
        }

        public static bool IsSpecialWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            return (SpecialWords.ContainsKey(word));
        }

        public static bool IsSpecialSymbol(string word)
        {
            if (string.IsNullOrEmpty(word) || word.Length > 1) { return false; }
            return SpecialSymbols.ContainsKey(word[0]);    
        }
        public static bool IsSpecialSymbol(char ch)
        {
            return SpecialSymbols.ContainsKey(ch);
        }

        public static bool IsDigit(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            return Int32.TryParse(word, out var digit);
        }
        public static bool IsDigit(char word)
        {
            if (word == null)
            {
                return false;
            }
            return Char.IsDigit(word);
        }

        public static bool IsLiteral(string word)
        {
            if (!string.IsNullOrEmpty(word) && word[0] == '"' && word[word.Length-1] == '"' && word.Length < 9)
            {
                return true;
            }
            return false;
        }


        public static bool IsIndentificator(string word)
        {
            bool CanBe = true;

            if (word.Length == 0 || !Char.IsLetter(word[0]) || word.Length > 8 || Char.IsDigit(word[0]))
                CanBe = false;
            else
                foreach (char s in word)
                {
                    if (s != '_' && !Char.IsDigit(s) && !Char.IsLetter(s))
                        CanBe = false;
                }
            return CanBe;
        }

        public static List<Token> LexAnalizing(string text)
        {

            List<Token> tokens = new List<Token>();
            string lexeme = "";

            text += " ";

            foreach(char ch in text)
            {
                if (lexeme == " " || lexeme == "\r") lexeme = "";
                else if (IsSpecialSymbol(lexeme))
                {
                    Token token = new Token(SpecialSymbols[lexeme[0]]);
                    token.Value = lexeme;
                    tokens.Add(token);
                    lexeme = "";
                }
                else if (IsSpecialWord(lexeme))
                {
                    Token token = new Token(SpecialWords[lexeme]);
                    token.Value = lexeme;
                    tokens.Add(token);
                    lexeme = "";
                }
                else if (IsDigit(lexeme) && (IsSpecialSymbol(ch) || ch == ' '))
                {
                    Token token = new Token(TokenType.LITERAL);
                    token.Value = lexeme;
                    tokens.Add(token);
                    lexeme = "";
                }
                
                else if (IsIndentificator(lexeme) && (ch == ' ' || IsSpecialSymbol(ch) || ch == '\n' || ch == '\r' ))
                {
                    Token token = new Token(TokenType.IDENTIFIER);
                    token.Value = lexeme;
                    tokens.Add(token);
                    lexeme = "";
                }
                else if (ch == ' ')
                {
                    if (lexeme.Length > 8)
                    {
                        throw new Exception($"Некорректный идентификатор \"{lexeme.Substring(0, lexeme.IndexOf(' '))}\"");
                    }
                }
                lexeme += ch;
            }


            return tokens;
        }

        


    }
}
