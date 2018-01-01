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
                    Generate((FunctionNode)node, instructions);
                    break;
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
            string registerL = GenerateExpression(node.left, instructions);
            string registerR = GenerateExpression(node.right, instructions);
            instructions.Add(new BinaryInstruction(node.op, registerL, registerR));
            return registerR;
        }

        private string GenerateIdentifier(IdentifierNode node, List<Instruction> instructions) {
            string r = GetRegister();
            instructions.Add(new MoveInstruction(node.identifier, r));
            return r;
        }

        private string GenerateNumber (NumberNode node, List<Instruction> instructions) {
            string r = GetRegister();
            instructions.Add(new MoveInstruction(node.value.ToString(), r));
            return r;
        }

        private string Generate(StatementNode node, List<Instruction> instructions) {
            string r = GenerateExpression(node.right, instructions);
            instructions.Add(new MoveInstruction(r, node.left.identifier));
            return r;
        }

        private void Generate(FunctionNode node, List<Instruction> instructions) {
            string name = node.functionName.identifier;
            List<string> parameters = new List<string>();
            foreach (ParameterNode param in node.parameters)
            {
                parameters.Add(param.identifier);
            }
            List<Instruction> funcInstructions = new List<Instruction>();
            foreach (StatementNode statement in node.body)
            {
                Generate(statement, funcInstructions);
            }
            Instruction returnInstruction = null;
            if (node.returnNode != null) {
                List<Instruction> returnInstructions = new List<Instruction>();
                GenerateExpression(node.returnNode.returnValue, returnInstructions);
                returnInstruction = returnInstructions[returnInstructions.Count - 1];
            }
            instructions.Add(new FunctionInstruction(name, parameters, funcInstructions, returnInstruction));
        }

        private String GetRegister() {
            string str = "R" + register;
            register++;
            return str;
        }

    }
}
