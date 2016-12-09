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
        


        public override string ToString()
        {
            return $"HusNr: {HusNr},NumberOfAdults {NumberAdults}, NumberOfKids 0-3 {NumberKidsZeroThree}, NumberOfKids 4-6 {NumberKidsFourSix}, NumberOfKids 7-15 {NUmberKidsSevenFifteen}";
        }

    }
}
