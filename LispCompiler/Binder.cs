using System;
using System.Collections.Generic;

namespace LispCompiler
{
    class Binder
    {
        Dictionary<string, string> globalEnvironment = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, string>> scopes = new Dictionary<string, Dictionary<string, string>>();

        private int varCount = 0;
        private int functionCount = 0;

        public SyntaxTree Bind(SyntaxTree tree)
        {
            List<SyntaxNode> nodes = new List<SyntaxNode>();
            for (int i = 0; i < tree.nodes.Count; i++)
            {
                nodes.Add(BindDeclaration(tree.nodes[i]));
            }

            return new SyntaxTree(nodes);
        }

        private SyntaxNode BindDeclaration(SyntaxNode node)
        {
            switch (node.type)
            {
                case SyntaxType.STATEMENT:
                    return Bind((StatementNode)node, globalEnvironment);
                case SyntaxType.FUNCTION:
                    return Bind((FunctionNode)node, globalEnvironment);
                default:
                    throw new Exception("Unexpected node");
            }
        }

        private SyntaxNode BindExpression(SyntaxNode node, Dictionary<string, string> environment)
        {
            switch (node.type)
            {
                case SyntaxType.STATEMENT:
                case SyntaxType.FUNCTION:
                    throw new Exception("Unexpected node");
                case SyntaxType.BINARY:
                    return BindBinary((BinaryNode)node, environment);
                case SyntaxType.NUMBER:
                    return BindNumber((NumberNode)node);
                case SyntaxType.IDENTIFIER:
                    return BindIdentifier((IdentifierNode)node, environment);
                case SyntaxType.CALL:
                    return BindCall((CallNode)node, environment); 
                default:
                    throw new NotImplementedException();
            }
        }

        private SyntaxNode BindCall(CallNode node, Dictionary<string, string> environment) {
            string name;
            if (!environment.TryGetValue(node.funcName, out name)) {
                throw new Exception("Unknow identifier: " + node.funcName);
            }
            List<SyntaxNode> boundValues = new List<SyntaxNode>();
            foreach (SyntaxNode val in node.values)
            {
                boundValues.Add(BindExpression(val, environment));
            }
            return new CallNode(name, boundValues);
        }

        private SyntaxNode BindBinary(BinaryNode node, Dictionary<string, string> environment) {
            SyntaxNode left = BindExpression(node.left, environment);
            SyntaxNode right = BindExpression(node.right, environment);
            return new BinaryNode(node.op, left, right);
        }

        private SyntaxNode BindNumber(NumberNode node){
            return node;  
        }

        private SyntaxNode BindIdentifier(IdentifierNode node, Dictionary<string, string> environment)
        {
            string name;
            if (!environment.TryGetValue(node.identifier, out name))
            {
                throw new Exception("Unknown identifier: " + node.identifier);
            }
            return new IdentifierNode(name);
        }

        private List<ParameterNode> BindParameters(List<ParameterNode> parameters, Dictionary<string, string> scope) {
            List<ParameterNode> nodes = new List<ParameterNode>();
            foreach (var parameter in parameters)
            {
                string identifier = parameter.identifier;
                string name = "a" + scope.Count;
                scope.Add(identifier, name);
                nodes.Add(new ParameterNode(name));
            }
            return nodes;
        }

        private ReturnNode BindReturn(ReturnNode node, Dictionary<string, string> environment) {
            SyntaxNode boundReturn = BindExpression(node.returnValue, environment);
            return new ReturnNode(boundReturn);
        }

        private StatementNode Bind(StatementNode node, Dictionary<string, string> environment)
        {
            SyntaxNode boundRHS = BindExpression(node.right, environment);
            string identifier = node.left.identifier;
            string name = "t" + GetVarCount();
            environment.Add(identifier, name);
            return new StatementNode(new IdentifierNode(name), boundRHS);
        }

        private SyntaxNode Bind(FunctionNode node, Dictionary<string, string> env)
        {
            Dictionary<string, string> localEnvironment = new Dictionary<string, string>();
            string name = "f" + GetFunctionCount();
            env.Add(node.functionName.identifier, name);
            List<ParameterNode> boundParams = BindParameters(node.parameters, localEnvironment);
            List<StatementNode> boundStatements = new List<StatementNode>();
            foreach (StatementNode statement in node.body)
            {
                StatementNode boundStatement = Bind(statement, localEnvironment);
                boundStatements.Add(boundStatement);
            }
            ReturnNode boundReturn = null;
            if (node.returnNode != null) {
                boundReturn = BindReturn(node.returnNode, localEnvironment);
            }
            return new FunctionNode(new IdentifierNode(name), boundParams, boundStatements, boundReturn);
        }

        private int GetVarCount() {
            int n = varCount;
            varCount++;
            return n;
        }

        private int GetFunctionCount() {
            int n = functionCount;
            functionCount++;
            return n;
        }
    }
}
