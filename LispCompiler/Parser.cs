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

        public SyntaxNode Parse()
        {
            return ParseExpression();
        }

        private SyntaxNode ParseExpression()
        {
            return null;
        }
    }
}

// goal
/*
 *           +
 *          / \
 *         21   *
 *            /  \
 *           2    4
 * 
 */
