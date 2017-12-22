using System;
namespace LispCompiler
{
    public class IdentifierNode : SyntaxNode
    {
        public string identifier;

        public IdentifierNode(string identifier) : base (SyntaxType.IDENTIFIER)
        {
            this.identifier = identifier;
        }

        public override string ToString()
        {
            return string.Format("[IdentifierNode]\n\tdata: {0}", identifier);
        }
    }
}
