namespace Qode.UI.Models
{
    public class SimulationQubit
    {
        public SimulationQubit()
        {
        }

        public SimulationQubit(int index, decimal probability)
        {
            Index = index;
            Probability = probability;
        }

        public int Index { get; set; }

        public decimal Probability { get; set; }

        public decimal Distribution { get; set; }
    }
}
