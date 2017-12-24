using System;
namespace LispCompiler
{
    public class ReturnNode : SyntaxNode
    {
        private SyntaxNode returnValue;

        public ReturnNode(SyntaxNode variable) : base (SyntaxType.RETURN) {
            this.returnValue = variable;
        }
    }
}
