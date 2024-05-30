using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Анализатор
{
    internal class TkType
    {
        public enum TokenType
        {
            INTEGER, STRING, BOOL, LITERAL, IDENTIFIER, END, TO,
            CASE, SELECT, DIM, AS, ELSE, TRUE, FALSE, LPAR, RPAR, PLUS,
            MINUS, EQUAL, MULTIPLY, ERROR, NEW_LINE, COMMA, DIVISION, EMPTY
        }

        public static TokenType[] Delimiters =
        {
            TokenType.PLUS, TokenType.MINUS,
            TokenType.EQUAL, TokenType.DIVISION, TokenType.RPAR, TokenType.LPAR
        };

        public static Dictionary<string, TokenType> SpecialWords = new Dictionary<string, TokenType>() {
                { "Integer", TokenType.INTEGER },
                { "As", TokenType.AS },
                { "Bool", TokenType.BOOL },
                { "Case", TokenType.CASE },
                { "Select", TokenType.SELECT },
                { "Dim", TokenType.DIM },
                { "String", TokenType.STRING },
                { "End", TokenType.END },
                { "Else", TokenType.ELSE },
                { "To", TokenType.TO }
        };

        public static Dictionary<char, TokenType> SpecialSymbols = new Dictionary<char, TokenType>() {
             { '(', TokenType.LPAR },
             { ')', TokenType.RPAR },
             { '+', TokenType.PLUS },
             { '-', TokenType.MINUS },
             { '=', TokenType.EQUAL },
             { ',', TokenType.COMMA },
             { '\n', TokenType.NEW_LINE },
             { '*', TokenType.MULTIPLY },
             { '/', TokenType.DIVISION }
        };
    }
}
