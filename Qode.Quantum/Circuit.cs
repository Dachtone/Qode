using System;
using System.Numerics;
using Qode.LinearAlgebra;

namespace Qode.Quantum
{
    // ToDo: Does not belong here
    public static class Util
    {
        public static IEnumerable<T> SliceRow<T>(this T[,] array, int row)
        {
            for (var i = array.GetLowerBound(1); i <= array.GetUpperBound(1); i++)
            {
                yield return array[row, i];
            }
        }
    }

    public class Circuit
    {
        private readonly Random _random;

        private Circuit()
        {
        }

        public Circuit(int numberOfQubits)
        {
            // ToDo: Collapse should be its own class
            _random = new Random();

            NumberOfQubits = numberOfQubits;

            // Start with the first qubit, compute a tensor product with subsequent ones
            OriginalState = new Qubit().Vector;
            for (int i = 1; i < numberOfQubits; i++)
            {
                OriginalState = OriginalState.TensorProduct(new Qubit().Vector);
            }

            State = OriginalState;
        }

        public int NumberOfQubits = 0;

        public Matrix<Complex> OriginalState { get; private set; }

        public List<Operation> Operations { get; private set; } = [];

        public Matrix<Complex>? U { get; private set; }

        public Matrix<Complex> State { get; private set; }

        // ToDo: Rename
        public void Set(Gate[,] operations)
        {
            for (int i = 0; i < operations.GetLength(0); i++)
            {
                Operation(operations.SliceRow(i).ToList());
            }
        }

        public void Operation(List<Gate> gates)
        {
            var gateMatricies = new List<Matrix<Complex>>();

            // Check if operation matches the number of qubits
            int qubits = 0;
            foreach (var gate in gates)
            {
                var gateMatrix = GateMatrix.Get(gate);

                gateMatricies.Add(gateMatrix);
                qubits += (int)Math.Log2(gateMatrix.Order);
            }

            if (qubits != NumberOfQubits)
            {
                // ToDo: Throw an exception
                return;
            }

            Operations.Add(new(gates));

            var operationMatrix = ComputeParallelGates(gateMatricies);
            if (U is null)
            {
                U = operationMatrix;
            }
            else
            {
                U = ComputeOperations(U, operationMatrix);
            }

            State = ComputeOperations(OriginalState, U);
        }

        public double[] GetProbabilities()
        {
            var probabilities = new double[State.Rows];
            for (int i = 0; i < State.Rows; i++)
            {
                probabilities[i] = Complex.Pow(Complex.Abs(State[i, 0]), 2).Real;
            }

            return probabilities;
        }

        public int Collapse()
        {
            double randomValue = _random.NextDouble();
            double probability = 0.0;
            for (int i = 0; i < State.Rows; i++)
            {
                probability += Complex.Pow(Complex.Abs(State[i, 0]), 2).Real;
                if (probability >= randomValue)
                {
                    return i;
                }
            }

            return -1;
        }

        private Matrix<Complex> ComputeParallelGates(IEnumerable<Matrix<Complex>> operations)
        {
            var operationMatrix = operations.First();
            for (int i = 1; i < operations.Count(); i++)
            {
                operationMatrix = operationMatrix.TensorProduct(operations.ElementAt(i));
            }

            return operationMatrix;
        }

        private Matrix<Complex> ComputeOperations(Matrix<Complex> first, Matrix<Complex> second)
        {
            return second.Multiply(first);
        }
    }
}
