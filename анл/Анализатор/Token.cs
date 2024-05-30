using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Анализатор.LexicalAnalyzer;
using static Анализатор.TkType;

namespace Анализатор
{
    class Token
    {
        public TokenType Type;
        public string Value;
        public int index = 0;
        public Token(TokenType type)
        {
            Type = type;
        }
        public Token(TokenType type, string value = null)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            if (Value == null || SpecialSymbols.ContainsKey(Value[0]) || SpecialWords.ContainsKey(Value))
                return $"{Type}";
            return $"{Value} - {Type}";
        }
    }
}
