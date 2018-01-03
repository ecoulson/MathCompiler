using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class FunctionInstruction : Instruction
    {
        public string name;
        public List<ParameterInstruction> parameters;
        public List<Instruction> body;
        public ReturnInstruction returnInstruction;

        public FunctionInstruction(string name) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = new List<ParameterInstruction>();
            this.body = new List<Instruction>();
            this.returnInstruction = null;
        }

        public FunctionInstruction(string name, List<ParameterInstruction> parameters) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = new List<Instruction>();
            this.returnInstruction = null;
        }

        public FunctionInstruction(string name, List<Instruction> instructions) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = new List<ParameterInstruction>();
            this.body = instructions;
            this.returnInstruction = null;
        }

        public FunctionInstruction(string name, List<ParameterInstruction> parameters, ReturnInstruction returnExpression) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = new List<Instruction>();
            this.returnInstruction = returnExpression;
        }

        public FunctionInstruction(string name, List<Instruction> instructions, ReturnInstruction returnExpression) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = new List<ParameterInstruction>();
            this.body = instructions;
            this.returnInstruction = returnExpression;
        }

        public FunctionInstruction(string name, List<ParameterInstruction> parameters, List<Instruction> instructions) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = instructions;
            this.returnInstruction = null;
        }

        public FunctionInstruction(string name, List<ParameterInstruction> parameters, List<Instruction> instructions, ReturnInstruction returnExpression) : base(InstructionType.FUNCTION)
        {
            this.name = name;
            this.parameters = parameters;
            this.body = instructions;
            this.returnInstruction = returnExpression;
        }
    }
}
