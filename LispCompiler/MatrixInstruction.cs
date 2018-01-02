using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class MatrixInstruction : Instruction
    {
        public string declaration;
        public MatrixInstruction(string declaration) : base(InstructionType.MATRIX)
        {
            this.declaration = declaration;
        }
   } 
}
