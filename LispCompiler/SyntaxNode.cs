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
    }
    
    public class SyntaxNode
    {
        public SyntaxType type;

        public SyntaxNode(SyntaxType type) {
            this.type = type;
        }
    }
}
