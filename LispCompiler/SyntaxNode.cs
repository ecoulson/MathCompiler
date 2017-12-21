using System;
namespace LispCompiler
{
    public class SyntaxNode
    {
        public Token token;
        public SyntaxNode left;
        public SyntaxNode right;

        public SyntaxNode(Token token)
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
