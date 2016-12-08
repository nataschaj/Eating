using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace Eating.Model
{
    public class TilmeldListe : ObservableCollection<Bolig>
    {
        public TilmeldListe() : base()
        {

            //this.Add(new Planlaeg() { Ret = "Kylling", ChefKok="Mathias", Hjaelpere = "Natascha", Oprydere="Rudi" });

            this.Add(new Bolig() { HusNr = 33, NumberAdults = 2, NumberKidsZeroThree = 1, NumberKidsFourSix = 3, NUmberKidsSevenFifteen = 1 });
        }




        /// <summary>
        /// Giver mig Json format for Tilmeld object
        /// </summary>
        /// <returns></returns>
        public string getJson()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        /// <summary>
        /// En methode som modtager en string af json og deserializerer til objekter. 
        /// </summary>
        /// <param name="jsonText"></param>
        public void IndsetJson(string jsonText)
        {
            List<Bolig> nyListe = JsonConvert.DeserializeObject<List<Bolig>>(jsonText);

            foreach (var BoligItem in nyListe)
            {
                this.Add(BoligItem);
            }
        }

    }
}
