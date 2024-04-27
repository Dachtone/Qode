using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qode.Quantum
{
    public class Operation
    {
        public Operation(IEnumerable<Gate> gates)
        {
            Gates = gates.ToList();
        }

        public List<Gate> Gates { get; private set; }
    }
}
