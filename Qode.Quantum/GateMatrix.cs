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

        private static readonly Dictionary<Gate, Matrix<Complex>> Matricies = new()
        {
            {
                Gate.Identity,
                new Complex[,]
                {
                    { 1, 0, },
                    { 0, 1, },
                }
            },
            {
                Gate.Hadamard,
                new Complex[,]
                {
                    { 1 / Math.Sqrt(2), 1 / Math.Sqrt(2), },
                    { 1 / Math.Sqrt(2), -1 / Math.Sqrt(2), },
                }
            },
            {
                Gate.CNot,
                new Complex[,]
                {
                    { 1, 0, 0, 0, },
                    { 0, 1, 0, 0, },
                    { 0, 0, 0, 1, },
                    { 0, 0, 1, 0, },
                }
            },
            {
                Gate.FlipCNot,
                new Complex[,]
                {
                    { 1, 0, 0, 0 },
                    { 0, 0, 0, 1 },
                    { 0, 0, 1, 0 },
                    { 0, 1, 0, 0 },
                }
            },
            {
                Gate.PauliX,
                new Complex[,]
                {
                    { 0, 1, },
                    { 1, 0, },
                }
            },
            {
                Gate.PauliY,
                new Complex[,]
                {
                    { new(0, 0), new(0, -1), },
                    { new(0, 1), new(0, 0), },
                }
            },
            {
                Gate.PauliZ,
                new Complex[,]
                {
                    { 1, 0, },
                    { 0, -1, },
                }
            },
            {
                Gate.SqrtNot,
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
                Gate.Swap,
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
