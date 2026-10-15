using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_RomanovK.S._BPI_24_01
{
    public class Formula5 : MainFunction
    {
        public int N { get; set; }
        public int K { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Formula5()
        {
            Source = Properties.Resources.f5;
        }

        public override double Calculate()
        {
            double Z = 0;
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= K; j++)
                {
                    double numerator = Math.Sin(Math.Pow(Y, j)) + i * X;
                    Z += numerator / ((i + 1) * j);
                }
            }
            return Z;
        }
    }
}
