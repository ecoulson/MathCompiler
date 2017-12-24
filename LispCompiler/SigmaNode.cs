using System;
namespace LispCompiler
{
    public class SigmaNode : SyntaxNode
    {
        public SyntaxNode expression;
        public NumberNode start;
        public NumberNode end;
        public NumberNode increment;
        public SigmaNode(SyntaxNode expression, NumberNode start, NumberNode end, NumberNode increment) : base(SyntaxType.SIGMA)
        {
            this.expression = expression;
            this.start = start;
            this.end = end;
            this.increment = increment;
        }
    }
}
