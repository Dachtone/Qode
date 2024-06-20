using Qode.Quantum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qode.Infrastructure.Models
{
    public class CircuitOperation
    {
        public required List<Gate> Gates { get; set; }
    }
}
