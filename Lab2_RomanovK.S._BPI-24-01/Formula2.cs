using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_RomanovK.S._BPI_24_01
{
    public class Formula2 : MainFunction
    {
        public double A { get; set; }
        public double B { get; set; }
        public double F { get; set; }

        public Formula2()
        {
            Source = Properties.Resources.f2;
        }

        public override double Calculate()
        {
            return Math.Cos(F * A) + Math.Sin(F * B);
        }
    }
}
