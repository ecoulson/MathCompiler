using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LispCompiler
{
    public enum TokenType {
        NUMBER,
        SUBTRACT,
        ADD,
        MULTIPLY,
        DIVIDE,
        RIGHT_PARENTHESES,
        LEFT_PARENTHESES,
        WHITE_SPACE,
        EOF,
    }

    public class Lexer
    {

        private const int SHIFT_LEFT = 10;
        private const int ASCII_ZERO = 48;
        private static Regex digitPattern = new Regex(@"\d");

        private StreamReader fileStream;

        public Lexer(string path)
        {
            this.fileStream = new StreamReader(path);
        }

        // returns a TokenStream comprised of all the tokens in the fileStream;
        public TokenStream Lex() {
            TokenStream tokenStream = new TokenStream();
            while(!fileStream.EndOfStream) {
                char ch = ReadChar();
                Token token = createToken(ch);

                if (token.type != TokenType.WHITE_SPACE) {
                    tokenStream.WriteToken(token);
                }
            }
            return tokenStream;
        }

        // returns a Token from a given char
        private Token createToken(char ch) {
            if (digitPattern.IsMatch(ch.ToString())) {
                return createNumberToken(ch);
            }
            switch (ch) {
                case '(':
                    return new Token(TokenType.LEFT_PARENTHESES);
                case ')':
                    return new Token(TokenType.RIGHT_PARENTHESES);
                case '-':
                    return new Token(TokenType.SUBTRACT);
                case '+':
                    return new Token(TokenType.ADD);
                case '*':
                    return new Token(TokenType.MULTIPLY);
                case '/':
                    return new Token(TokenType.DIVIDE);
                default:
                    return new Token(TokenType.WHITE_SPACE);
            }
        }

        private Token createNumberToken(char ch) {
            int n = ReadNumber(ch);
            return new Token(TokenType.NUMBER, n);
        }

        private int ReadNumber(char ch) {
            int digit = ch - ASCII_ZERO;
            return ReadNumber(digit);
        }

        private int ReadNumber(int n) {
            char ch = PeekChar();
            if (digitPattern.IsMatch(ch.ToString())) {
                n *= SHIFT_LEFT;
                ch = ReadChar();
                int digit = ch - ASCII_ZERO;
                return ReadNumber(n + digit);
            }
            return n;
        }


        // returns next char in filestream;
        private char ReadChar() {
            return (char)fileStream.Read();
        }

        private char PeekChar() {
            return (char)fileStream.Peek();
        }
    }
}
