using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Eating.ViewModel
{
    public class TIlmeldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Model.Bolig NyBolig { get; set; }
        public Model.TilmeldListe TimmeldListen { get; set; }

        public TIlmeldViewModel()
        {
            TimmeldListen = new Model.TilmeldListe();
        }
    }
}
