using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_RomanovK.S._BPI_24_01
{
    public class Formula4 : MainFunction
    {
        public double A { get; set; }
        public int D { get; set; }
        public double C { get; set; }

        public Formula4()
        {
            Source = Properties.Resources.f4;
        }

        public override double Calculate()
        {
            double res = 1.0;
            for (int i = 0; i < D; i++)
            {
                res = res * (C + A) + 1;
            }
            return res;
        }
    }
}
