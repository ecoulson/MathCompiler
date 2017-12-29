using System;
namespace LispCompiler
{
    public class ReturnNode : SyntaxNode
    {
        public BinaryNode returnValue;

        public ReturnNode(BinaryNode variable) : base (SyntaxType.RETURN) {
            this.returnValue = variable;
        }

        public override string ToString()
        {
            return string.Format("[ReturnNode] {0}", returnValue);
        }
    }
}
