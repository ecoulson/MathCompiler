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
        PARAMETER,
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
