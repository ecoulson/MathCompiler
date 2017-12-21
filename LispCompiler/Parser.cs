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

        public ParseNode Parse()
        {
            return ParseExpression();
        }

        private ParseNode ParseExpression()
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
