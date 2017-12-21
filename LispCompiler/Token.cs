using System;
namespace LispCompiler
{
    public class Token
    {
        public TokenType type;
        public int number;
        public bool hasNumber;

        public Token(TokenType type)
        {
            this.type = type;
            this.number = int.MinValue;
            this.hasNumber = false;
        }

        public Token(TokenType type, int number)
        {
            this.type = type;
            this.number = number;
            this.hasNumber = true;
        }

        public override string ToString()
        {
            return String.Format(
                "[Token] type: {0}, number: {1} hasNumber: {2}",
                type,
                number,
                hasNumber
            );
        }
    };
}
