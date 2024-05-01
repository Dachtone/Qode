using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qode.Quantum
{
    public interface IComputationalResource
    {
        public void RegisterQubits(int count);

        public void Operation(IEnumerable<Gate> gates);

        public bool Measure(int qubitIndex);
    }
}
