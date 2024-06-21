namespace Qode.UI.Utils
{
    public static class StateDisplay
    {
        public static string FromIndex(int index, int numberOfQubits = -1)
        {
            var result = Convert.ToString(index, 2);
            return result.PadLeft(numberOfQubits, '0');
        }
    }
}
