using System;
namespace LispCompiler
{
    public class ParseNode
    {
        public Token token;
        public ParseNode left;
        public ParseNode right;

        public ParseNode(Token token)
        {
            this.token = token;
        }

        public override string ToString()
        {
            return string.Format(
                "\n[ParseNode]\n\ttoken: {0}\n\tleft: {1}\n\tright: {2}\n",
                token,
                left,
                right
            );
        }
    }
}
