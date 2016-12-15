using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage;

namespace Eating.ViewModel
{
    class AfregningViewModel : INotifyPropertyChanged
    {
        StorageFolder localfolder = null;
        private readonly string filnavn = "mandagJson.json";
        public event PropertyChangedEventHandler PropertyChanged;

        /*Uge lister*/
        public Model.TilmeldListe TimmeldListenMandag { get; set; }

        ///*Count number of adults in the first json object*/
        public double NUmberOfAdultsMandag
        {
            get
            {
                /* return TimmeldListenMandag.Sum(p => p.NumberAdults);*/
                return TimmeldListenMandag.getKuvurter();
            }
        }

 


        private double antalKuverterPerUge;

        public double AntalKuverterPerUge
        {
            get { return antalKuverterPerUge; }
            set { antalKuverterPerUge = value; }
        }



        /*Constructor*/
        public AfregningViewModel()
        {
            TimmeldListenMandag = new Model.TilmeldListe();
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
         
        }

        /*Methods*/

        public double BeregnKuverterMandag()
        {
            TimmeldListenMandag.getKuvurter();
             antalKuverterPerUge = TimmeldListenMandag.getKuvurter();
            return AntalKuverterPerUge;

        }



        public async void HentDataFraDiskAsync()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(filnavn);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TimmeldListenMandag.Clear();
                TimmeldListenMandag.IndsetJson(jsonText);
                OnPropertyChanged("NUmberOfAdultsMandag");
            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }
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
