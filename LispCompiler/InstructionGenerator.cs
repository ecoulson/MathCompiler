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
            Console.WriteLine(instructions[instructions.Count - 1]);
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
                case SyntaxType.OUT:
                    Generate((OutNode)node, instructions);
                    break;
                default:
                    throw new Exception("Unexpected Node");
            }
        }

        private string Generate(OutNode node, List<Instruction> instructions) {
            string outExp = "";
            GenerateReturn(node.outputExpression, ref outExp);
            instructions.Add(new OutInstruction(outExp));
            return outExp;
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
                case SyntaxType.CALL:
                    return GenerateCall((CallNode)node, instructions);
                case SyntaxType.MATRIX:
                    return GenerateMatrix((MatrixNode)node, instructions);
                default:
                    throw new Exception("Unexpected Node");
            }
        }

        private string GenerateMatrix(MatrixNode matrix, List<Instruction> instructions) {
            //TODO Rework
            string r = GetRegister();
            string matrixString = GenerateMatrixString(matrix);
            instructions.Add(new MatrixInstruction(matrixString, r));
            return matrixString;
        }


        private string GenerateMatrixString(MatrixNode node) {
            string matrixString = "{";
            for (int i = 0; i < node.values.Count; i++)
            {
                SyntaxNode n = node.values[i];
                switch (n.type) {
                    case SyntaxType.MATRIX:
                        matrixString += GenerateMatrixString((MatrixNode)n);
                        break;
                    default:
                        GenerateReturn(n, ref matrixString);
                        break;
                }
                if (i < node.values.Count - 1)
                {
                    matrixString += ",";
                }
            }
            matrixString += "}";
            return matrixString;
        }

        private string GenerateCall(CallNode node, List<Instruction> instructions) {
            string r = GetRegister();
            string callString = node.funcName + "(";
            for (int i = 0; i < node.values.Count; i++) {
                callString += GenerateExpression(node.values[i], instructions);
                if (i == node.values.Count - 1) {
                    callString += ")";
                } else {
                    callString += ",";
                }
            }
            instructions.Add(new MoveInstruction(callString, r));
            return r;
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
            List<ParameterInstruction> parameters = new List<ParameterInstruction>();
            foreach (ParameterNode param in node.parameters)
            {
                parameters.Add(new ParameterInstruction(param.identifier));
            }
            List<Instruction> funcInstructions = new List<Instruction>();
            foreach (StatementNode statement in node.body)
            {
                Generate(statement, funcInstructions);
            }
            ReturnInstruction returnInstruction = null;
            if (node.returnNode != null) {
                List<Instruction> returnInstructions = new List<Instruction>();
                returnInstruction = GenerateReturn(node.returnNode);
            }
            instructions.Add(new FunctionInstruction(name, parameters, funcInstructions, returnInstruction));
        }

        private ReturnInstruction GenerateReturn(ReturnNode node) {
            SyntaxNode current = node.returnValue;
            string returnExpression = "";
            GenerateReturn(current, ref returnExpression);
            return new ReturnInstruction(returnExpression);
        }

        private void GenerateReturn(SyntaxNode node, ref string expression) {
            switch (node.type) {
                case SyntaxType.BINARY:
                    BinaryNode binaryNode = (BinaryNode)node;
                    GenerateReturn(binaryNode.left, ref expression);
                    expression += opToString(binaryNode.op);
                    GenerateReturn(binaryNode.right, ref expression);
                    break;
                case SyntaxType.NUMBER:
                    NumberNode num = (NumberNode)node;
                    expression += num.value.ToString();
                    break;
                case SyntaxType.IDENTIFIER:
                    IdentifierNode identifier = (IdentifierNode)node;
                    expression += identifier.identifier;
                    break;
                default:
                    throw new Exception("Unexpected Node");
            }
        }

        private string opToString(Operator op) {
            switch (op) {
                case Operator.ADDITION:
                    return " + ";
                case Operator.SUBTRACTION:
                    return " - ";
                case Operator.MULTIPLICATION:
                    return " * ";
                case Operator.DIVISION:
                    return " / ";
                default:
                    throw new Exception("Unexpected Operator");
            }
        }

        private String GetRegister() {
            string str = "R" + register;
            register++;
            return str;
        }

    }
}
