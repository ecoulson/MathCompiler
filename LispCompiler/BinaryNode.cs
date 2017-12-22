using System;
namespace LispCompiler
{
    public enum Operator {
        MULTIPLICATION,
        DIVISION,
        ADDITION,
        SUBTRACTION,
    }

    public class BinaryNode : SyntaxNode
    {
        public Operator op;
        public SyntaxNode left;
        public SyntaxNode right;

        public BinaryNode(Operator op) : base(SyntaxType.BINARY)
        {
            this.op = op;
            left = null;
            right = null;
        }


        public BinaryNode(Operator op, SyntaxNode left, SyntaxNode right) : base(SyntaxType.BINARY)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }

        public override string ToString()
        {
            return string.Format(
                "\n[BinaryNode]\n\toperator: {0}\n\tleft: {1}\n\tright: {2}\n",
                op,
                left,
                right
            );
        }
    }
}
