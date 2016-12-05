using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Eating.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
       public Model.PlanlaegListe PlanlaegListe { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public MainViewModel()
        {
            PlanlaegListe = new Model.PlanlaegListe();
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
