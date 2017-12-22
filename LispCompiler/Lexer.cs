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
        SEMI_COLON,
        ASSIGNMENT,
        IDENTIFIER,
        EOF,
    }

    public class Lexer
    {
        private static Regex DigitPattern = new Regex(@"\d");
        private static Regex WhiteSpacePattern = new Regex(@"[ \t\n\r]+");
        private static Regex LetterPattern = new Regex(@"[a-zA-Z]");

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
                Token token = CreateToken(ch);

                if (token.type != TokenType.WHITE_SPACE) {
                    tokenStream.WriteToken(token);
                }
            }
            return tokenStream;
        }

        // returns a Token from a given char
        private Token CreateToken(char ch) {
            if (DigitPattern.IsMatch(ch.ToString())) {
                return CreateNumberToken(ch);
            }
            if (WhiteSpacePattern.IsMatch(ch.ToString())) {
                return new Token(TokenType.WHITE_SPACE);
            }
            if (LetterPattern.IsMatch(ch.ToString())) {
                return CreateIdentifierToken(ch);
            }
            if (ch == ':') {
                char next = PeekChar();
                if (next == '=')
                {
                    ReadChar();
                    return new Token(TokenType.ASSIGNMENT);
                }
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
                case ';':
                    return new Token(TokenType.SEMI_COLON);
                default:
                    throw new Exception("Unrecognized token of " + ch);
                    
            }
        }

        private Token CreateNumberToken(char ch) {
            string data = ReadPattern(ch + "", DigitPattern);
            return new Token(TokenType.NUMBER, data);
        }

        private Token CreateIdentifierToken(char ch) {
            string data = ReadPattern(ch + "", LetterPattern);
            return new Token(TokenType.IDENTIFIER, data);
        }

        private string ReadPattern(String identifier, Regex pattern) {
            char ch = PeekChar();
            if (pattern.IsMatch(ch.ToString()))
            {
                identifier += ch;
                ch = ReadChar();
                return ReadPattern(identifier, pattern);
            }
            return identifier;
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
