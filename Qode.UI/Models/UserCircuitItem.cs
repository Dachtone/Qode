using Qode.Infrastructure.Models;

namespace Qode.UI.Models
{
    public class UserCircuitItem
    {
        public int Id;
        public string? Name;
        public string? AuthorName;
        public bool IsOwn;
        public bool IsFavorite;
        public IEnumerable<CircuitOperation> Circuit = [];
    }
}
