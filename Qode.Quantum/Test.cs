using Qode.LinearAlgebra;
using System.Numerics;

namespace Qode.Quantum
{
    public class Test
    {
        public void Go()
        {
            var circuit = new Circuit(2);
            circuit.Operation([Gate.H, Gate.I]);
            circuit.Operation([Gate.CNOT]);

            var probabilities = circuit.GetProbabilitiesOfStates();

            var statistics = new double[circuit.State.Rows];
            var shots = 1000;
            for (int i = 0; i < shots; i++)
            {
                var collapsedIndex = circuit.Collapse();
                if (collapsedIndex == -1)
                {
                    // Bug! Invalid state
                    break;
                }

                statistics[collapsedIndex] += 1.0 / shots;
            }
        }
    }
}
