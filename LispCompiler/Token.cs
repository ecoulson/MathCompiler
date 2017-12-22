using System;
namespace LispCompiler
{
    public class Token
    {
        public TokenType type;
        public string data;

        public Token(TokenType type)
        {
            this.type = type;
            this.data = "";
        }

        public Token(TokenType type, string data)
        {
            this.type = type;
            this.data = data;
        }

        public override string ToString()
        {
            return String.Format(
                "[Token] type: {0}, data: {1}",
                type,
                data
            );
        }
    };
}
