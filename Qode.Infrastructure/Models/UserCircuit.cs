using Qode.Quantum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qode.Infrastructure.Models
{
    public class UserCircuit
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required IEnumerable<CircuitOperation> CircuitOperations { get; set; }

        public ApplicationUser? Author { get; set; }

        public List<ApplicationUser> UserFavorites { get; set; } = [];
    }
}
