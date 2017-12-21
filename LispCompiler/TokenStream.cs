using System;
using System.Collections.Generic;
using System.IO;

namespace LispCompiler
{
    public class TokenStream
    {
        private const int BASE_CAPACITY = 128;
        private Queue<Token> tokenStream;

        public TokenStream()
        {
            this.tokenStream = new Queue<Token>();
        }

        public Token ReadToken() {
            return tokenStream.Dequeue();
        } 

        public void WriteToken(Token token) {
            tokenStream.Enqueue(token);
        }

        public Token PeekToken() {
            return tokenStream.Peek();
        }

        public int Length() {
            return tokenStream.Count;
        }

        public override string ToString()
        {
            string str = "";
            Token[] tokens = tokenStream.ToArray();
            foreach (Token token in tokens) {
                str += token.ToString() + ", ";
            }
            return str;
        }
    }
}
