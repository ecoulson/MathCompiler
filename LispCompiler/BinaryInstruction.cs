using System;
namespace LispCompiler
{
    public class BinaryInstruction : Instruction
    {
        public Operator op;
        public string arg1;
        public string arg2;

        public BinaryInstruction(Operator op, string arg1, string arg2) : base(InstructionType.BINARY)
        {
            this.op = op;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public override string ToString()
        {
            return string.Format("[BinaryInstruction] " +
                                 "{0} {1} {2}", op, arg1, arg2);
        }
    }
}
