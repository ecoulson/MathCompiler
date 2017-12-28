using System;
using System.Collections.Generic;

namespace LispCompiler
{
    public class InstructionGenerator
    {
        private SyntaxTree tree;
        private int register;

        public InstructionGenerator(SyntaxTree tree)
        {
            this.register = 0;
            this.tree = tree;
        }

        public List<Instruction> Generate() {
            List<Instruction> instructions = new List<Instruction>();
            foreach (SyntaxNode node in tree.nodes)
            {
                GenerateDeclaration(node, instructions);
            }
            return instructions;
        }

        private void GenerateDeclaration(SyntaxNode node, List<Instruction> instructions) {
            switch (node.type)
            {
                case SyntaxType.STATEMENT:
                    Generate((StatementNode)node, instructions);
                    break;
                case SyntaxType.FUNCTION:
                    throw new NotImplementedException();
                default:
                    throw new Exception("Unexpected Node");
            }
        }

        private string GenerateExpression(SyntaxNode node, List<Instruction> instructions) {
            switch (node.type)
            {
                case SyntaxType.FUNCTION:
                case SyntaxType.STATEMENT:
                    throw new Exception("Unexpected Node");
                case SyntaxType.BINARY:
                    return GenerateBinary((BinaryNode)node, instructions);
                case SyntaxType.IDENTIFIER:
                    return GenerateIdentifier((IdentifierNode) node, instructions);
                case SyntaxType.NUMBER:
                    return GenerateNumber((NumberNode) node, instructions);
                default:
                    throw new Exception("Unexpected Node");
            }
        }

        private string GenerateBinary(BinaryNode node, List<Instruction> instructions) {
            InstructionType type = Instruction.GetInstructionTypeFromOperator(node.op);
            string registerL = GenerateExpression(node.left, instructions);
            string registerR = GenerateExpression(node.right, instructions);
            instructions.Add(new Instruction(type, registerL, registerR));
            return registerR;
        }

        private string GenerateIdentifier(IdentifierNode node, List<Instruction> instructions) {
            string r = GetRegister();
            InstructionType type = InstructionType.MOVE;
            instructions.Add(new Instruction(type, node.identifier, r));
            return r;
        }

        private string GenerateNumber (NumberNode node, List<Instruction> instructions) {
            string r = GetRegister();
            InstructionType type = InstructionType.MOVE;
            instructions.Add(new Instruction(type, node.value.ToString(), r));
            return r;
        }

        private string Generate(StatementNode node, List<Instruction> instructions) {
            string r = GenerateExpression(node.right, instructions);
            InstructionType type = InstructionType.MOVE;
            instructions.Add(new Instruction(type, r, node.left.identifier));
            return r;
        }

        private String GetRegister() {
            string str = "R" + register;
            register++;
            return str;
        }

    }
}
