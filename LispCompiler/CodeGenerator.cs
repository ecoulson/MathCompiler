using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class CodeGenerator
    {
        private SyntaxTree tree;

        public CodeGenerator(SyntaxTree tree)
        {
            this.tree = tree;
        }

        public List<Instruction> generate() {
            return null;
        }
    }
}
