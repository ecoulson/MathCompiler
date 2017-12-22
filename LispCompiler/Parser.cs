using System;
namespace LispCompiler
{
    public class Parser
    {
        private TokenStream tokenStream;

        public Parser(TokenStream tokenStream)
        {
            this.tokenStream = tokenStream;
        }

        public SyntaxNode Parse() {
            return ReadStatement();
        }

        private SyntaxNode ReadStatement() {
            IdentifierNode variable = ReadIdentifier();
            StatementNode assignment = ReadAssignment();
            SyntaxNode expression = ReadExpression();
            assignment.left = variable;
            assignment.right = expression;
            return assignment;
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
                    opNode.right = root;
                    opNode.left = ReadTerm();
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
            SyntaxNode root = ReadFactor();
            if (tokenStream.Length() > 0) {
                Token peek = tokenStream.PeekToken();
                while (isTermOperator(peek.type))
                {
                    Token token = tokenStream.ReadToken();
                    Operator op = GetOperator(token);
                    BinaryNode opNode = new BinaryNode(op);
                    opNode.right = root;
                    opNode.left = ReadFactor();
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

        // number or left paren
        private SyntaxNode ReadFactor() {
            Token token = tokenStream.ReadToken();

            switch (token.type) {
                case TokenType.NUMBER:
                    return new NumberNode(token.data);

                case TokenType.LEFT_PARENTHESES:
                    SyntaxNode expressionNode = ReadExpression();
                    Token next = tokenStream.PeekToken();
                    if (next.type == TokenType.RIGHT_PARENTHESES) {
                        return expressionNode;
                    } else {
                        string error = "Expected TokenType.RIGHT_PARENTHESES instead found TokenType." + 
                                        token.type.ToString();
                        throw new Exception(error);
                    }

                default:
                    string message = String.Format(
                        "Invalid Token of type \"{0}\" expected TokenType.NUMBER or TokenType.LEFT_PARENTHESES",
                        token.type
                    );
                    throw new Exception(message);
            }
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
