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
        public string Dag { get; set; }
        public DateTime OpretDato { get; set; }
        public string OpretDatoText { get {
                return OpretDato.ToString("d-M-yyyy");
            } }


        //public string thisDay
        //{
        //    get { return DateTime.Now.ToString("d-s-yyyy");}
        //    set {  }
        //}


        public override string ToString()
        {
            //return "Ret: " + Ret + " \n " + "Chef kok: " + ChefKok + "\n  "+ "Hjælpere: " + Hjaelpere + "\n  " + "Opryddere: " + Oprydere + " \n ";
            return $"Ret: {Ret}, Chef kok: {ChefKok}, \n Hjælpere: {Hjaelpere}, Opryddere: {Oprydere}, Dag: {Dag}" ;
        }
    }
}
