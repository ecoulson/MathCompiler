using System;
namespace LispCompiler
{
    public class OutInstruction : Instruction
    {
        public string exp;
        public OutInstruction(string exp) : base(InstructionType.OUT)
        {
            this.exp = exp;
        }
    }
}
