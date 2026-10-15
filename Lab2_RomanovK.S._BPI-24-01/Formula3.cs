using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_RomanovK.S._BPI_24_01
{
    public class Formula3 : MainFunction
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }

        public Formula3()
        {
            Source = Properties.Resources.f3;
        }

        public override double Calculate()
        {
            return C * A * A + D * B * B;
        }
    }
}
