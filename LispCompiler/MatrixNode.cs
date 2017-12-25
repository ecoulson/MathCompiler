using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class MatrixNode : SyntaxNode
    {
        public List<SyntaxNode> values;

        public MatrixNode(List<SyntaxNode> values) : base(SyntaxType.MATRIX)
        {
            this.values = values;
        }

        public override string ToString()
        {
            return string.Format("[MatrixNode]");
        }
    }
}
