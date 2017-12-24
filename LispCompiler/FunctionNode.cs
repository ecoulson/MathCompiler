using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class FunctionNode : SyntaxNode
    {
        public IdentifierNode functionName;
        public List<ParameterNode> parameters;
        public List<StatementNode> body;
        public ReturnNode returnNode;

        public FunctionNode(IdentifierNode name, List<ParameterNode> parameters) : base (SyntaxType.FUNCTION)
        {
            this.functionName = name;
            this.parameters = parameters;
            this.body = null;
            this.returnNode = null;
        }

        public FunctionNode(IdentifierNode name, List<ParameterNode> parameters, List<StatementNode> body) : base(SyntaxType.FUNCTION)
        {
            this.functionName = name;
            this.parameters = parameters;
            this.body = body;
            this.returnNode = null;
        }

        public FunctionNode(IdentifierNode name, List<ParameterNode> parameters, ReturnNode returnNode) : base(SyntaxType.FUNCTION)
        {
            this.functionName = name;
            this.parameters = parameters;
            this.body = null;
            this.returnNode = returnNode;
        }

        public FunctionNode(IdentifierNode name, List<ParameterNode> parameters, List<StatementNode> body, ReturnNode returnNode) : base(SyntaxType.FUNCTION)
        {
            this.functionName = name;
            this.parameters = parameters;
            this.body = body;
            this.returnNode = returnNode;
        }
    }
}
