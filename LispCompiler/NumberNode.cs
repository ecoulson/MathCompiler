using System;
namespace LispCompiler
{
    public class NumberNode : SyntaxNode
    {
        public int value;

        public NumberNode(string value) : base(SyntaxType.NUMBER)
        {
            this.value = Int32.Parse(value);    
        }

        public override string ToString()
        {
            return string.Format(
                "\n[NumberNode]\n\tvalue: {0}",
                value
            );
        }
    }
}
