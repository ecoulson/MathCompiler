using System;
using System.Collections.Generic;

namespace LispCompiler
{
    class Binder
    {
        Dictionary<String, String> globalEnvironment = new Dictionary<String, String>();

        public SyntaxTree Bind(SyntaxTree tree)
        {
            List<SyntaxNode> nodes = new List<SyntaxNode>();
            for (int i = 0; i < tree.nodes.Count; i++)
            {
                nodes.Add(BindDeclaration(tree.nodes[i]));
            }

            return new SyntaxTree(nodes);
        }

        public SyntaxNode BindDeclaration(SyntaxNode node)
        {
            switch (node.type)
            {
                case SyntaxType.STATEMENT:
                    return Bind((StatementNode)node);
                case SyntaxType.FUNCTION:
                    return Bind((FunctionNode)node);
                default:
                    throw new Exception("Unexpected node");
            }
        }

        public SyntaxNode BindExpression(SyntaxNode node)
        {
            switch (node.type)
            {
                case SyntaxType.STATEMENT:
                case SyntaxType.FUNCTION:
                    throw new Exception("Unexpected node");
                case SyntaxType.BINARY:
                    return BindBinary((BinaryNode)node);
                case SyntaxType.NUMBER:
                    return BindNumber((NumberNode)node);
                case SyntaxType.IDENTIFIER:
                    return BindIdentifier((IdentifierNode)node);
                default:
                    throw new NotImplementedException();
            }
        }

        public SyntaxNode BindBinary(BinaryNode node) {
            SyntaxNode left = BindExpression(node.left);
            SyntaxNode right = BindExpression(node.right);
            return new BinaryNode(node.op, left, right);
        }

        public SyntaxNode BindNumber(NumberNode node){
            return node;  
        }

        public SyntaxNode BindIdentifier(IdentifierNode node)
        {
            string name;
            if (!this.globalEnvironment.TryGetValue(node.identifier, out name))
            {
                throw new Exception("Unknown identifier: " + node.identifier);
            }

            return new IdentifierNode(name);
        }

        public SyntaxNode Bind(StatementNode node)
        {
            SyntaxNode boundRHS = BindExpression(node.right);
            string identifier = node.left.identifier;
            string name = "t" + this.globalEnvironment.Count.ToString();
            globalEnvironment.Add(identifier, name);
            return new StatementNode(new IdentifierNode(name), boundRHS);
        }

        public SyntaxNode Bind(FunctionNode node)
        {
            throw new NotImplementedException();
        }
    }
}
