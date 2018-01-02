using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class Parser
    {
        private TokenStream tokenStream;

        public Parser(TokenStream tokenStream)
        {
            this.tokenStream = tokenStream;
        }

        public SyntaxTree Parse() {
            List<SyntaxNode> main = new List<SyntaxNode>();
            SyntaxTree tree = new SyntaxTree(main);
            while (tokenStream.Length() != 0) {
                Token token = tokenStream.PeekToken();
                switch (token.type) {
                    case TokenType.IDENTIFIER:
                        main.Add(ReadStatement());
                        break;
                    case TokenType.FUNCTION:
                        tokenStream.ReadToken();
                        main.Add(ReadFunction());
                        break;
                    default:
                        break;
                }
            }
            return tree;
        }

        private FunctionNode ReadFunction () {
            IdentifierNode functionName = ReadIdentifier();
            List<ParameterNode> parameters = ReadParameters();
            if (tokenStream.ReadToken().type != TokenType.LEFT_BRACE) {
                throw new Exception("Expected Left Brace");
            }
            List<StatementNode> body = ReadStatements();
            ReturnNode returnNode = ReadReturn();
            if (tokenStream.ReadToken().type != TokenType.RIGHT_BRACE)
            {
                throw new Exception("Expected Right Brace");
            }
            return new FunctionNode(functionName, parameters, body, returnNode);
        }

        private ReturnNode ReadReturn() {
            Token token = tokenStream.PeekToken();
            if (token.type != TokenType.RETURN) {
                return null;
            }
            token = tokenStream.ReadToken();
            SyntaxNode node = ReadExpression();
            return new ReturnNode(node);
        }

        private List<StatementNode> ReadStatements() {
            Token token = tokenStream.PeekToken();
            List<StatementNode> body = new List<StatementNode>();
            while (token.type == TokenType.IDENTIFIER)
            {
                body.Add(ReadStatement());
                token = tokenStream.PeekToken();
            }
            return body;
        }

        private List<ParameterNode> ReadParameters() {
            List<ParameterNode> nodes = new List<ParameterNode>();
            Token leftParen = tokenStream.ReadToken();
            if (leftParen.type != TokenType.LEFT_PARENTHESES) {
                throw new Exception("expected LEFT_PAREN to follow function name");
            }
            Token next = tokenStream.ReadToken();
            while (next.type != TokenType.RIGHT_PARENTHESES) {
                string paramName = next.data;
                nodes.Add(new ParameterNode(paramName));
                next = tokenStream.ReadToken();
                if (next.type == TokenType.COMMA) {
                    next = tokenStream.ReadToken();
                }
            }
            return nodes;
        }

        private StatementNode ReadStatement() {
            IdentifierNode variable = ReadIdentifier();
            StatementNode assignment = ReadAssignment();
            SyntaxNode expression = ReadExpression();
            assignment.left = variable;
            assignment.right = expression;
            return assignment;
        }

        private SyntaxNode ReadSet()
        {
            List<SyntaxNode> nodes = new List<SyntaxNode>();
            Token token = tokenStream.PeekToken();
            while (token.type != TokenType.RIGHT_BRACE)
            {
                if (token.type == TokenType.LEFT_BRACE)
                {
                    tokenStream.ReadToken();
                    nodes.Add(ReadSet());
                }
                else
                {
                    nodes.Add(ReadExpression());
                }
                token = tokenStream.ReadToken();
            }
            return new SetNode(nodes);
        }

        private SyntaxNode ReadMatrix()
        {
            List<SyntaxNode> nodes = new List<SyntaxNode>();
            Token token = tokenStream.PeekToken();
            while (token.type != TokenType.RIGHT_BRACKET)
            {
                if (token.type == TokenType.RIGHT_BRACE)
                {
                    tokenStream.ReadToken();
                    nodes.Add(ReadMatrix());
                }
                else
                {
                    nodes.Add(ReadExpression());
                }
                token = tokenStream.ReadToken();
            }
            return new MatrixNode(nodes);
        }

        private SyntaxNode ReadSigma() {
            SyntaxNode expression = ReadExpression();
            NumberNode start = new NumberNode("0");
            NumberNode end = new NumberNode("-1");
            NumberNode increment = new NumberNode("1");
            if (tokenStream.PeekToken().type == TokenType.NUMBER) {
                Token token = tokenStream.ReadToken();
                start = new NumberNode(token.data);
            }
            if (tokenStream.PeekToken().type == TokenType.NUMBER)
            {
                Token token = tokenStream.ReadToken();
                end = new NumberNode(token.data);
            }
            if (tokenStream.PeekToken().type == TokenType.NUMBER)
            {
                Token token = tokenStream.ReadToken();
                increment = new NumberNode(token.data);
            }
            return new SigmaNode(expression, start, end, increment);
        }

        private IdentifierNode ReadIdentifier() {
            Token token = tokenStream.ReadToken();
            if (token.type == TokenType.IDENTIFIER) {
                return new IdentifierNode(token.data);
            } else {
                throw new Exception("Expected Identifier Token instead got " + token.type);
            }
        }

        private StatementNode ReadAssignment() {
            Token token = tokenStream.ReadToken();
            if (token.type == TokenType.ASSIGNMENT) {
                return new StatementNode();
            } else {
                throw new Exception("Expected Assignment Token instead got " + token.type);
            }

        }

        // + or -
        private SyntaxNode ReadExpression()
        {
            SyntaxNode root = ReadTerm();
            if (tokenStream.Length() > 0) {
                Token peek = tokenStream.PeekToken();
                while (isExpressionOperator(peek.type))
                {
                    Token token = tokenStream.ReadToken();
                    Operator op = GetOperator(token);
                    BinaryNode opNode = new BinaryNode(op);
                    opNode.left = root;
                    opNode.right = ReadTerm();
                    root = opNode;
                    if (tokenStream.Length() == 0) {
                        return root;
                    }
                    peek = tokenStream.PeekToken();
                }
            }
            return root;
        }

        // * or /
        private SyntaxNode ReadTerm() {
            SyntaxNode root = ReadExponent();
            if (tokenStream.Length() > 0) {
                Token peek = tokenStream.PeekToken();
                while (isTermOperator(peek.type))
                {
                    Token token = tokenStream.ReadToken();
                    Operator op = GetOperator(token);
                    BinaryNode opNode = new BinaryNode(op);
                    opNode.right = root;
                    opNode.left = ReadExponent();
                    root = opNode;

                    if (tokenStream.Length() == 0)
                    {
                        return root;
                    }
                    peek = tokenStream.PeekToken();
                }
            }
            return root;
        }

        private SyntaxNode ReadExponent() {
            SyntaxNode root = ReadFactor();
            if (tokenStream.Length() > 0) {
                Token peek = tokenStream.PeekToken();
                while (peek.type == TokenType.EXPONENT) {
                    Token token = tokenStream.ReadToken();
                    Operator op = GetOperator(token);
                    BinaryNode opNode = new BinaryNode(op);
                    opNode.right = root;
                    opNode.left = ReadFactor();
                    root = opNode;

                    if (tokenStream.Length() == 0) {
                        return root;
                    }
                    peek = tokenStream.PeekToken();
                }
            }
            return root;
        }

        // number or left paren
        private SyntaxNode ReadFactor() {
            Token token = tokenStream.ReadToken();
            switch (token.type) {
                case TokenType.NUMBER:
                    return new NumberNode(token.data);
                case TokenType.IDENTIFIER:
                    if (tokenStream.PeekToken().type == TokenType.LEFT_PARENTHESES)
                    {
                        return ReadFunctionCall(token.data);
                    } else {
                        return new IdentifierNode(token.data);    
                    }
                case TokenType.LEFT_BRACE:
                    return ReadSet();
                case TokenType.LEFT_BRACKET:
                    return ReadMatrix();
                case TokenType.SIGMA:
                    return ReadSigma();
                case TokenType.LEFT_PARENTHESES:
                    SyntaxNode expressionNode = ReadExpression();
                    Token next = tokenStream.ReadToken();
                    if (next.type == TokenType.RIGHT_PARENTHESES) {
                        return expressionNode;
                    } else {
                        string error = "Expected TokenType.RIGHT_PARENTHESES instead found TokenType." + 
                                        token.type.ToString();
                        throw new Exception(error);
                    }

                default:
                    string message = String.Format(
                        "Invalid Token of type \"{0}\"",
                        token.type
                    );
                    throw new Exception(message);
            }
        }

        private SyntaxNode ReadFunctionCall(string functionName) {
            tokenStream.ReadToken();
            Token peek = tokenStream.PeekToken();
            List<SyntaxNode> paramNodes = new List<SyntaxNode>();
            while(peek.type != TokenType.RIGHT_PARENTHESES) {
                paramNodes.Add(ReadExpression());
                peek = tokenStream.PeekToken();
                tokenStream.ReadToken();
            }
            return new CallNode(functionName, paramNodes);
        }

        private Operator GetOperator(Token token) {
            switch (token.type) {
                case TokenType.ADD:
                    return Operator.ADDITION;
                case TokenType.SUBTRACT:
                    return Operator.SUBTRACTION;
                case TokenType.DIVIDE:
                    return Operator.DIVISION;
                case TokenType.MULTIPLY:
                    return Operator.MULTIPLICATION;
                case TokenType.EXPONENT:
                    return Operator.EXPONENTIATE;
                default:
                    throw new Exception("Unknown Operator");
            }
        }

        private static bool isTermOperator(TokenType type) {
            return type == TokenType.MULTIPLY || type == TokenType.DIVIDE;
        }

        private static bool isExpressionOperator(TokenType type) {
            return type == TokenType.ADD || type == TokenType.SUBTRACT;
        }
    }
}
