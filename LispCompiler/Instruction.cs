using System;
namespace LispCompiler
{
    public enum InstructionType {
        ADD,
        SUBTRACT,
        DIVIDE,
        MULTIPLY,
        MOVE,
    }

    public class Instruction
    {
        public InstructionType type;
        public string arg1;
        public string arg2;

        public Instruction(InstructionType type, string arg1, string arg2)
        {
            this.type = type;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public static InstructionType GetInstructionTypeFromOperator(Operator op) {
            switch (op)
            {
                case Operator.ADDITION:
                    return InstructionType.ADD;
                case Operator.SUBTRACTION:
                    return InstructionType.SUBTRACT;
                case Operator.DIVISION:
                    return InstructionType.DIVIDE;
                case Operator.MULTIPLICATION:
                    return InstructionType.MULTIPLY;
                default:
                    throw new Exception("Invalid Operator");
            }
        }

        public override string ToString()
        {
            return string.Format("[Instruction] " +
                                 "{0} {1} {2}", type, arg1, arg2);
        }
    }
}
