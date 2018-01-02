using System;
namespace LispCompiler
{
    public class ReturnInstruction : Instruction
    {
        public string expression;

        public ReturnInstruction(string expression) : base (InstructionType.RETURN)
        {
            this.expression = expression;    
        }
    }
}
