using System;
namespace LispCompiler
{
    public enum SyntaxType
    {
        FUNCTION,
        PARAMETER,
        RETURN,
        STATEMENT,
        BINARY,
        NUMBER,
        IDENTIFIER,
        SET,
        MATRIX,
        SIGMA,
        CALL,
    }
    
    public class SyntaxNode
    {
        public SyntaxType type;

        public SyntaxNode(SyntaxType type) {
            this.type = type;
        }

        public override string ToString()
        {
            return string.Format("[SyntaxNode] type: {0}", type);
        }
    }
}
