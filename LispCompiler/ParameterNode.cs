using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class ParameterNode : SyntaxNode
    {
        private string identifier;

        public ParameterNode(string identifier) : base (SyntaxType.PARAMETER)
        {
            this.identifier = identifier;
        }
    }
}
