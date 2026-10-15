using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_RomanovK.S._BPI_24_01
{
    public class Formula1 : MainFunction
    {
        public double A { get; set; }
        public double F { get; set; }

        public Formula1()
        {
            Source = Properties.Resources.f1;
        }

        public override double Calculate()
        {
            return Math.Sin(F * A);
        }
    }
}
