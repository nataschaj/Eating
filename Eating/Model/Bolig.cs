using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating.Model
{
    public class Bolig
    {
        public int HusNr { get; set; }
        public int NumberAdults { get; set; }
        public int NumberKidsZeroThree { get; set; }
        public int NumberKidsFourSix { get; set; }
        public int NUmberKidsSevenFifteen { get; set; }

        public double KuvertProcentAdults { get; set; }
        public double KuvertProcentZeroThree { get; set; }
        public double KuvertProcentFourSIx { get; set; }
        public double KuvertProcentSevenFifteen { get; set; }

        public Bolig()
        {
            this.KuvertProcentAdults = 1;

        }

        public override string ToString()
        {
            return $"{HusNr} {NumberAdults} {NumberKidsZeroThree} {NumberKidsFourSix} {NUmberKidsSevenFifteen}";
        }

    }
}
