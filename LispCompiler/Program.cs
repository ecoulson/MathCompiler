using System;

namespace LispCompiler
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Lexer lex = new Lexer("test_files/test.math");
            TokenStream tokenStream = lex.Lex();
            Parser parser = new Parser(tokenStream);
            SyntaxTree syntaxTree = parser.Parse();
            Console.WriteLine(syntaxTree.nodes.Count);
        }
    }
}
