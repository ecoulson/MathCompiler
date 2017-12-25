using System;
namespace LispCompiler
{
    public class StatementNode : SyntaxNode
    {
        public IdentifierNode left;
        public SyntaxNode right;

        public StatementNode() : base (SyntaxType.STATEMENT) {
            
        }

        public StatementNode(IdentifierNode left, SyntaxNode right) : base(SyntaxType.STATEMENT)
        {
            this.left = left;
            this.right = right;
        }

        public override string ToString()
        {
            return string.Format("[StatementNode]\n\tleft: {0}\n\tright: {1}", left, right);
        }
    }
}
