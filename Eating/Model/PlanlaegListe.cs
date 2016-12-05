using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Eating.Model
{
    public class PlanlaegListe : ObservableCollection<Planlaeg>
    {
        public PlanlaegListe() : base()
        {
            //this.Add(new Planlaeg() { Ret = "Kylling", ChefKok="Mathias", Hjaelpere = "Natascha", Oprydere="Rudi" });
        }

        /// <summary>
        /// Giver mig Json format for Planlegningsliste object
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
            List<Planlaeg> nyListe = JsonConvert.DeserializeObject<List<Planlaeg>>(jsonText);

            foreach (var PlanlaegItem in nyListe)
            {
                this.Add(PlanlaegItem);
            }
        }
    }
}
