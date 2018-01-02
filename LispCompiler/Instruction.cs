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
        OUT,
        MATRIX,
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
