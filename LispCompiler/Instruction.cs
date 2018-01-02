using System;
namespace LispCompiler
{
    public enum InstructionType
    {
        MOVE,
        BINARY,
        FUNCTION,
        RETURN,
        CALL,
    }

    public class Instruction
    {
        public InstructionType type;

        public Instruction(InstructionType type)
        {
            this.type = type;
        }
    }
}
