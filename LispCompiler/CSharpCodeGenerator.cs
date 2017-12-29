using System;
using System.Collections.Generic;
using System.IO;

namespace LispCompiler
{
    public class CSharpCodeGenerator
    {
        private const string header = "using System;\npublic class Test {\npublic static void Main(string[] args) {\n";
        private const string footer = "Console.WriteLine(R8);}\n}";

        private StreamWriter output;
        private List<Instruction> instructions;
        private HashSet<string> vars;

        public CSharpCodeGenerator(List<Instruction> instructions)
        {
            this.instructions = instructions;
            this.output = new StreamWriter(MainClass.OutputDirectory + "a.cs");
            this.vars = new HashSet<string>();
        }

        public void Generate() {
            output.Write(header);
            foreach (Instruction instruction in instructions)
            {
                string generatedCode = Generate(instruction);
                output.Write(generatedCode);
            }

            output.Write(footer);
            output.Close();
        }

        public string Generate(Instruction instruction) {
            switch (instruction.type)
            {
                case InstructionType.MOVE:
                    return GenerateAssignment(instruction, vars.Contains(instruction.arg2));
                case InstructionType.ADD:
                    return GenerateOperation(instruction, "+=");
                case InstructionType.SUBTRACT:
                    return GenerateOperation(instruction, "-=");
                case InstructionType.DIVIDE:
                    return GenerateOperation(instruction, "/=");
                case InstructionType.MULTIPLY:
                    return GenerateOperation(instruction, "*=");
                default:
                    return "";
            }
        }

        private string GenerateAssignment(Instruction instruction, bool isDeclared) {
            if (isDeclared) {
                return instruction.arg2 + " = " + instruction.arg1 + ";\n";
            } else {
                vars.Add(instruction.arg2);
                return "double " + instruction.arg2 + " = " + instruction.arg1 + ";\n";
            }
        }

        private string GenerateOperation(Instruction instruction, string op) {
            return instruction.arg2 + " " + op + " " + instruction.arg1 + ";\n";
        }

    }
}
