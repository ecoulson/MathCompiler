using System;
namespace LispCompiler
{
    public class MoveInstruction : Instruction
    {
        public string arg1;
        public string arg2;

        public MoveInstruction(string arg1, string arg2) : base (InstructionType.MOVE)
        {
            this.arg1 = arg1;
            this.arg2 = arg2;
        }
    }
}
