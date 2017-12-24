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
        EXPONENT,
        RIGHT_PARENTHESES,
        LEFT_PARENTHESES,
        RIGHT_BRACE,
        LEFT_BRACE,
        LEFT_BRACKET,
        RIGHT_BRACKET,
        WHITE_SPACE,
        COMMA,
        ASSIGNMENT,
        IDENTIFIER,
        FUNCTION,
        RETURN,
        SIGMA,
    }

    public static class Keywords {
        public const string FUNCTION = "function";
        public const string RETURN = "return";
        public const string SIGMA = "sigma";
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
                string value = ReadPattern(ch + "", LetterPattern);
                switch(value) { 
                    case Keywords.FUNCTION:
                        return new Token(TokenType.FUNCTION);
                    case Keywords.RETURN:
                        return new Token(TokenType.RETURN);
                    case Keywords.SIGMA:
                        return new Token(TokenType.SIGMA);
                    default:
                        return new Token(TokenType.IDENTIFIER, value);
                }
            }
            if (ch == ':') {
                char next = PeekChar();
                if (next == '=')
                {
                    ReadChar();
                    return new Token(TokenType.ASSIGNMENT);
                } else {
                    throw new Exception("No token of type ':'");
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
                case '^':
                    return new Token(TokenType.EXPONENT);
                case ',':
                    return new Token(TokenType.COMMA);
                case '{':
                    return new Token(TokenType.LEFT_BRACE);
                case '}':
                    return new Token(TokenType.RIGHT_BRACE);
                case '[':
                    return new Token(TokenType.LEFT_BRACKET);
                case ']':
                    return new Token(TokenType.RIGHT_BRACKET);
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
