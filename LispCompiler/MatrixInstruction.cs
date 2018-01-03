using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class MatrixInstruction : Instruction
    {
        public string arg1;
        public string arg2;
        public MatrixInstruction(string arg1, string arg2) : base(InstructionType.MATRIX)
        {
            this.arg1 = arg1;
            this.arg2 = arg2;
        }
   } 
}
