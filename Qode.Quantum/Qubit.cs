using System.Numerics;
using Qode.LinearAlgebra;

namespace Qode.Quantum
{
    public class Qubit
    {
        public Qubit(bool set = false)
        {
            if (set)
            {
                Vector = new Complex[,]
                {
                    { 0 },
                    { 1 },
                };
            }
            else
            {
                Vector = new Complex[,]
                {
                    { 1 },
                    { 0 },
                };
            }
        }

        public Matrix<Complex> Vector { get; set; }
    }
}
