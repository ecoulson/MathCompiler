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
            string nodesString = "";

            foreach (SyntaxNode node in nodes) {
                nodesString += node.ToString() + "\n";
            }

            return string.Format("[SyntaxTree] nodes: {0}", nodesString);
        }
    }
}
