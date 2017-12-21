using System;
namespace LispCompiler
{
    public class Parser
    {
        private TokenStream tokenStream;
        private ParseNode head;

        public Parser(TokenStream tokenStream)
        {
            this.tokenStream = tokenStream;
            this.head = null;
        }

        public ParseNode Parse() {
            return ParseExpression();
        }

        private ParseNode ParseExpression() {
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
