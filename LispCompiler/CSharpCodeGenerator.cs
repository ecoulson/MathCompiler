﻿using System;
using System.Collections.Generic;
using System.IO;

namespace LispCompiler
{
    public class CSharpCodeGenerator
    {
        private const string header = "using System;\npublic class Test {\npublic static void Main(string[] args) {\n";
        private const string footer = "}\n";

        private StreamWriter output;
        private List<Instruction> instructions;
        private HashSet<string> vars;
        private Queue<string> functions;

        public CSharpCodeGenerator(List<Instruction> instructions)
        {
            this.instructions = instructions;
            this.functions = new Queue<string>();
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
            while (functions.Count > 0)
            {
                output.Write(functions.Dequeue());
            }
            output.Write("}");
            output.Close();
        }

        public string Generate(Instruction instruction) {
            switch (instruction.type)
            {
                case InstructionType.MOVE:
                    return GenerateAssignment((MoveInstruction)instruction);
                case InstructionType.BINARY:
                    return GenerateBinary((BinaryInstruction)instruction);
                case InstructionType.FUNCTION:
                    functions.Enqueue(GenerateFunction((FunctionInstruction)instruction));
                    return "";
                case InstructionType.OUT:
                    return GenerateOut((OutInstruction)instruction);
                default:
                    throw new Exception("unrecognized instruction type");
            }
        }

        private string GenerateOut(OutInstruction instruction) {
            return "Console.WriteLine(" + instruction.exp + ");";
        }

        private string GenerateAssignment(MoveInstruction instruction) {
            bool isDeclared = vars.Contains(instruction.arg2);
            if (isDeclared) {
                return instruction.arg2 + " = " + instruction.arg1 + ";\n";
            } else {
                vars.Add(instruction.arg2);
                return "var " + instruction.arg2 + " = " + instruction.arg1 + ";\n";
            }
        }

        private string GenerateBinary(BinaryInstruction instruction) {
            switch (instruction.op) {
                case Operator.ADDITION:
                    return GenerateOperation(instruction, "+=");
                case Operator.SUBTRACTION:
                    return GenerateOperation(instruction, "-=");
                case Operator.DIVISION:
                    return GenerateOperation(instruction, "/=");
                case Operator.MULTIPLICATION:
                    return GenerateOperation(instruction, "*=");
                default:
                    throw new Exception("unrecognzed operator");
            }
        }

        private string GenerateOperation(BinaryInstruction instruction, string op) {
            return instruction.arg2 + " " + op + " " + instruction.arg1 + ";\n";
        }

        private string GenerateFunction(FunctionInstruction instruction) {
            string paramString = GenerateParams(instruction.parameters);
            string returnType = instruction.returnInstruction == null ? "void" : "double";
            string funcHeader = "\nprivate static " + returnType + " " + instruction.name + "(" + paramString + ") {\n";
            string body = "";
            foreach (Instruction bodyInstruction in instruction.body)
            {
                body += Generate(bodyInstruction);
            }
            if (instruction.returnInstruction != null) {
                body += "return " + instruction.returnInstruction.expression + ";";
            }
            string funcFooter = "\n}\n";
            return funcHeader + body + funcFooter;
        }

        private string GenerateParams(List<string> parameters) {
            string paramString = "";
            for (int i = 0; i < parameters.Count; i++) {
                paramString += "var " + parameters[i];
                if (i != parameters.Count - 1) {
                    paramString += ", ";
                }
            }
            return paramString;
        }

    }
}
