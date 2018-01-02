using System;
using System.Collections.Generic;
namespace LispCompiler
{
    public class CallNode : SyntaxNode
    {
        public string funcName;
        public List<SyntaxNode> values;
        public CallNode(string funcName, List<SyntaxNode> values) : base (SyntaxType.CALL)
        {
            this.funcName = funcName;
            this.values = values;
        }
    }
}
