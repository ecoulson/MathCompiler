using System;
using System.Collections.Generic;

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
            Binder binder = new Binder();
            SyntaxTree boundTree = binder.Bind(syntaxTree);
            CodeGenerator generator = new CodeGenerator(boundTree);
            List<Instruction> instrucitons = generator.generate();
            Console.WriteLine(instrucitons.Count);
        }
    }

}
