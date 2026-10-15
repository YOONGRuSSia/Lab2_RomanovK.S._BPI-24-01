using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_RomanovK.S._BPI_24_01
{
    public abstract class MainFunction
    {
        public byte[] Source { get; protected set; }

        public abstract double Calculate();
    }
}
