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
        private readonly Random _random = new Random();

        private Circuit()
        {
        }

        public Circuit(int numberOfQubits)
        {
            NumberOfQubits = numberOfQubits;
            Initialize();
        }

        public Circuit(int numberOfQubits, List<List<Gate>> circuit)
        {
            NumberOfQubits = numberOfQubits;
            Initialize();

            Load(circuit);
        }

        public static readonly int MaxNumberOfQubits = 12;

        public int NumberOfQubits = 0;

        public Matrix<Complex> OriginalState { get; private set; }

        public List<Operation> Operations { get; private set; } = [];

        public Matrix<Complex>? U { get; private set; }

        public Matrix<Complex> State { get; set; }

        private void Initialize()
        {
            // Start with the first qubit, compute a tensor product with subsequent ones
            OriginalState = new Qubit().Vector;
            for (int i = 1; i < NumberOfQubits; i++)
            {
                OriginalState = OriginalState.TensorProduct(new Qubit().Vector);
            }

            State = OriginalState;
        }

        public void Load(List<List<Gate>> circuit)
        {
            foreach (var operation in circuit)
            {
                Operation(operation);
            }
        }

        public void Operation(Gate[,] operations)
        {
            for (int i = 0; i < operations.GetLength(0); i++)
            {
                Operation(operations.SliceRow(i).ToList());
            }
        }

        public void Operation(IEnumerable<Gate> gates)
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

        public double[] GetProbabilitiesOfStates()
        {
            var probabilities = new double[State.Rows];
            for (int i = 0; i < State.Rows; i++)
            {
                probabilities[i] = Complex.Pow(Complex.Abs(State[i, 0]), 2).Real;
            }

            return probabilities;
        }

        public double[] GetProbabilitiesOfQubits()
        {
            var probabilities = new double[NumberOfQubits];
            for (int i = 0; i < NumberOfQubits; i++)
            {
                int power = (int)Math.Pow(2, NumberOfQubits - 1 - i);

                // Probability of bit 1
                int bit = 1;

                double probability = 0.0;
                for (int j = 0; j < State.Rows; j++)
                {
                    // Skip if state is not related to the bit
                    // i.e. measuring for q1 = 0, skip 10, 11 
                    if (j / power % 2 != bit)
                    {
                        continue;
                    }

                    probability += Complex.Pow(Complex.Abs(State[j, 0]), 2).Real;
                }

                probabilities[i] = probability;
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

        // qubitIndex starts with 0
        public bool Measure(int qubitIndex)
        {
            int power = (int)Math.Pow(2, NumberOfQubits - 1 - qubitIndex);
            
            // Choose a bit to sum the probabilities. For example, 0
            int bit = 0;

            double randomValue = _random.NextDouble();
            double probability = 0.0;
            for (int i = 0; i < State.Rows; i++)
            {
                // Skip if state is not related to the bit
                // i.e. measuring for q1 = 0, skip 10, 11 
                if (i / power % 2 != bit)
                {
                    continue;
                }

                probability += Complex.Pow(Complex.Abs(State[i, 0]), 2).Real;
            }

            // If the measured bit is the opposite of what we calculated
            if (probability < randomValue)
            {
                // Flip the bit, flip the probability
                bit = 1;
                probability = 1 - probability;
            }

            var norm = Math.Sqrt(probability);
            for (int i = 0; i < State.Rows; i++)
            {
                if (i / power % 2 == bit)
                {
                    // Normalize the state
                    State[i, 0] /= norm;
                }
                else
                {
                    // There's no probability for this state since we measured the qubit to be in an opposite state
                    State[i, 0] = 0;
                }
            }

            // True for 1, false for 0
            return bit == 1;
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
