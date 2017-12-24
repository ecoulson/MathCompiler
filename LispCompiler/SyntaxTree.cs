using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class SyntaxTree
    {
        public List<SyntaxNode> nodes;

        public SyntaxTree(List<SyntaxNode> nodes)
        {
            this.nodes = nodes;
        }

        public override string ToString()
        {
            return string.Format("[SyntaxTree] nodes: {0}", nodes);
        }
    }
}
