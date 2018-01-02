using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class CallInstruction : Instruction
    {
        string funcName;
        List<string> arguments;
        public CallInstruction(string funcName, List<string> arguments) : base(InstructionType.CALL)
        {
            this.funcName = funcName;
            this.arguments = arguments;
        }
    }
}
