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
    

        public double kuverterPerBolig()
        {
            double Sum = NumberAdults + (NumberKidsFourSix * 0.25) + (NUmberKidsSevenFifteen * 0.5);

            return Sum;
        }


        public double BeregnPris(double KuvertPris)
        {
            return KuvertPris / kuverterPerBolig()  ;
        }

        public override string ToString()
        {
            return $"{HusNr} {NumberAdults} {NumberKidsZeroThree} {NumberKidsFourSix} {NUmberKidsSevenFifteen} ";
        }

    }
}
