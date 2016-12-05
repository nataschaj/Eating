using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Eating.Model
{
    public class PlanlaegListe : ObservableCollection<Planlaeg>
    {
        public PlanlaegListe() : base()
        {
            this.Add(new Planlaeg() { Ret = "Kylling", ChefKok="Mathias", Hjaelpere = "Natascha", Oprydere="Rudi" });
        }
    }
}
