using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class SetNode : SyntaxNode
    {
        public List<SyntaxNode> values;

        public SetNode(List<SyntaxNode> values) : base(SyntaxType.SET)
        {
            this.values = values;
        }
    }
}
