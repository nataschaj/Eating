using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eating.Model
{
    public class Planlaeg
    {
        public string Ret { get; set; }
        public string ChefKok { get; set; }
        public string Hjaelpere { get; set; }
        public string Oprydere { get; set; }

        public override string ToString()
        {
            return "Ret: " + Ret + " \n " + "Chef kok: " + ChefKok + "\n  "+ "Hjælpere: " + Hjaelpere + "\n  " + "Opryddere: " + Oprydere + " \n ";
        }
    }
}
