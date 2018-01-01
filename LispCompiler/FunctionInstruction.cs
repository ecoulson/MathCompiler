using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class FunctionInstruction : Instruction
    {
        public string name;
        public List<String> parameters;
        public List<Instruction> body;
        public Instruction returnInstruction;

        public FunctionInstruction(string name) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = new List<string>();
            this.body = new List<Instruction>();
            this.returnInstruction = null;
        }

        public FunctionInstruction(string name, List<String> parameters) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = new List<Instruction>();
            this.returnInstruction = null;
        }

        public FunctionInstruction(string name, List<Instruction> instructions) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = new List<string>();
            this.body = instructions;
            this.returnInstruction = null;
        }

        public FunctionInstruction(string name, List<String> parameters,Instruction returnExpression) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = new List<Instruction>();
            this.returnInstruction = returnExpression;
        }

        public FunctionInstruction(string name, List<Instruction> instructions, Instruction returnExpression) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = new List<string>();
            this.body = instructions;
            this.returnInstruction = returnExpression;
        }

        public FunctionInstruction(string name, List<String> parameters, List<Instruction> instructions) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = instructions;
            this.returnInstruction = null;
        }

        public FunctionInstruction(string name, List<String> parameters, List<Instruction> instructions, Instruction returnExpression) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = instructions;
            this.returnInstruction = returnExpression;
        }
    }
}
