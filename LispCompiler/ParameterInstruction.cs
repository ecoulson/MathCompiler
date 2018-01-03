using System;
namespace LispCompiler
{
    public class ParameterInstruction : Instruction
    {
        public string parameter;
        public ParameterInstruction(string parameter) : base(InstructionType.PARAMETER)
        {
            this.parameter = parameter;
        }
    }
}
