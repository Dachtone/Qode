using Qode.Infrastructure.Models;
using Qode.Quantum;
using Qode.UI.Models;

namespace Qode.UI.Utils
{
    public static class CircuitTransformer
    {
        public static List<List<CircuitGate>> Transform(IEnumerable<CircuitOperation> circuit)
        {
            var transformedCircuit = new List<List<CircuitGate>>();
            foreach (var operation in circuit)
            {
                transformedCircuit.Add(new(
                    operation.Gates.Select(e => new CircuitGate()
                    {
                        Gate = e,
                        Span = GateMatrix.GetOperatorCount(e),
                    })));
            }

            return transformedCircuit;
        }

        public static List<List<CircuitGate>> Transform(List<List<Gate>> circuit)
        {
            var transformedCircuit = new List<List<CircuitGate>>();
            foreach (var operation in circuit)
            {
                transformedCircuit.Add(new(
                    operation.Select(e => new CircuitGate()
                    {
                        Gate = e,
                        Span = GateMatrix.GetOperatorCount(e),
                    })));
            }

            return transformedCircuit;
        }
    }
}
