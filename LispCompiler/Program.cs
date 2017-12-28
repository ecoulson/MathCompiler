using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LispCompiler
{
    class MainClass
    {
        public const string OutputDirectory = "/Library/MathLang";

        public static void Main(string[] args)
        {
            // Math to C#
            Lexer lex = new Lexer("/Users/evancoulson/github/MathCompiler/LispCompiler/test_files/test.math");
            TokenStream tokenStream = lex.Lex();
            Parser parser = new Parser(tokenStream);
            SyntaxTree syntaxTree = parser.Parse();
            Binder binder = new Binder();
            SyntaxTree boundTree = binder.Bind(syntaxTree);
            InstructionGenerator instructionGenerator = new InstructionGenerator(boundTree);
            List<Instruction> instructions = instructionGenerator.Generate();
            CSharpCodeGenerator codeGenerator = new CSharpCodeGenerator(instructions);
            codeGenerator.Generate();

            // execute C#
            string compilerPath = "/Library/Frameworks/Mono.framework/Versions/5.4.1/lib/mono/4.5/csc.exe";
            string compilerArgs = String.Format("/out:{0}/a.exe {0}/a.cs", OutputDirectory);
            Process compilation = Process.Start(compilerPath, compilerArgs);
            compilation.WaitForExit();
            Process execution = Process.Start("mono", String.Format("{0}/a.exe", OutputDirectory));
            execution.WaitForExit();
        }
    }

}
