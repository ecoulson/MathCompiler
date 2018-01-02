using System;
namespace LispCompiler
{
    public class OutNode : SyntaxNode
    {
        public SyntaxNode outputExpression;
        public OutNode(SyntaxNode outputExpression) : base (SyntaxType.OUT)
        {
            this.outputExpression = outputExpression;
        }
    }
}
