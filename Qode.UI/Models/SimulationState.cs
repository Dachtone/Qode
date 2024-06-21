namespace Qode.UI.Models
{
    public class SimulationState
    {
        public SimulationState()
        {
        }

        public SimulationState(int index, decimal value)
        {
            Index = index;
            Value = value;
        }

        public int Index { get; set; }

        public decimal Value { get; set; }
    }
}
