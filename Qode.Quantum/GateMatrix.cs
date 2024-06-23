using System.Numerics;
using Qode.LinearAlgebra;

namespace Qode.Quantum
{
    public static class GateMatrix
    {
        public static Matrix<Complex> Get(Gate gate)
        {
            return Matricies[gate];
        }

        public static int GetOperatorCount(Gate gate) => (int)Math.Log2(Get(gate).Order);

        private static readonly Dictionary<Gate, Matrix<Complex>> Matricies = new()
        {
            {
                Gate.I,
                new Complex[,]
                {
                    { 1, 0, },
                    { 0, 1, },
                }
            },
            {
                Gate.H,
                new Complex[,]
                {
                    { 1 / Math.Sqrt(2), 1 / Math.Sqrt(2), },
                    { 1 / Math.Sqrt(2), -1 / Math.Sqrt(2), },
                }
            },
            {
                Gate.CNOT,
                new Complex[,]
                {
                    { 1, 0, 0, 0, },
                    { 0, 1, 0, 0, },
                    { 0, 0, 0, 1, },
                    { 0, 0, 1, 0, },
                }
            },
            {
                Gate.NOTC,
                new Complex[,]
                {
                    { 1, 0, 0, 0 },
                    { 0, 0, 0, 1 },
                    { 0, 0, 1, 0 },
                    { 0, 1, 0, 0 },
                }
            },
            {
                Gate.X,
                new Complex[,]
                {
                    { 0, 1, },
                    { 1, 0, },
                }
            },
            {
                Gate.PY,
                new Complex[,]
                {
                    { new(0, 0), new(0, -1), },
                    { new(0, 1), new(0, 0), },
                }
            },
            {
                Gate.PZ,
                new Complex[,]
                {
                    { 1, 0, },
                    { 0, -1, },
                }
            },
            {
                Gate.SX,
                new Complex[,]
                {
                    { new Complex(1, 1) / 2, new Complex(1, -1) / 2, },
                    { new Complex(1, -1) / 2, new Complex(1, 1) / 2, },
                }
            },
            {
                Gate.S,
                new Complex[,]
                {
                    { 1, 0, },
                    { 0, new(0, 1), },
                }
            },
            {
                Gate.T,
                new Complex[,]
                {
                    { 1, 0, },
                    { 0, Complex.Exp(new(0, Math.PI / 4)), },
                }
            },
            {
                Gate.SWAP,
                new Complex[,]
                {
                    { 1, 0, 0, 0, },
                    { 0, 0, 1, 0, },
                    { 0, 1, 0, 0, },
                    { 0, 0, 0, 1, },
                }
            },
            {
                Gate.Toffoli,
                new Complex[,]
                {
                    { 1, 0, 0, 0, 0, 0, 0, 0, },
                    { 0, 1, 0, 0, 0, 0, 0, 0, },
                    { 0, 0, 1, 0, 0, 0, 0, 0, },
                    { 0, 0, 0, 1, 0, 0, 0, 0, },
                    { 0, 0, 0, 0, 1, 0, 0, 0, },
                    { 0, 0, 0, 0, 0, 1, 0, 0, },
                    { 0, 0, 0, 0, 0, 0, 0, 1, },
                    { 0, 0, 0, 0, 0, 0, 1, 0, },
                }
            },
            {
                Gate.Fredkin,
                new Complex[,]
                {
                    { 1, 0, 0, 0, 0, 0, 0, 0, },
                    { 0, 1, 0, 0, 0, 0, 0, 0, },
                    { 0, 0, 1, 0, 0, 0, 0, 0, },
                    { 0, 0, 0, 1, 0, 0, 0, 0, },
                    { 0, 0, 0, 0, 1, 0, 0, 0, },
                    { 0, 0, 0, 0, 0, 0, 1, 0, },
                    { 0, 0, 0, 0, 0, 1, 0, 0, },
                    { 0, 0, 0, 0, 0, 0, 0, 1, },
                }
            },
        };
    }
}
