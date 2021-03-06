﻿using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class ParameterNode : SyntaxNode
    {
        public string identifier;

        public ParameterNode(string identifier) : base (SyntaxType.PARAMETER)
        {
            this.identifier = identifier;
        }

        public override string ToString()
        {
            return string.Format("[ParameterNode]");
        }
    }
}
