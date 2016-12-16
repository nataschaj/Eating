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


        private int hentHusnummer;
        public int HentHusnummer
        {
            get { return hentHusnummer; }
            set { hentHusnummer = value; OnPropertyChanged(nameof(HentHusnummer)); OnPropertyChanged(nameof(hentKuverter));
            }
        }

        public double hentKuverter { get {
                return KuverterPerBolig();
            }
        }

        private int hetPris;

        public int HentPris
        {
            get { return hetPris; }
            set { hetPris = value; OnPropertyChanged(nameof(HentPris)); OnPropertyChanged(nameof(visPris)); }
        }

        public int visPris { get {
                return PrisPerBolig();
            } }



        private double antalKuverterPerDag;

        public double AntalKuverterPerDag
        {
            get { return antalKuverterPerDag; }
            set { antalKuverterPerDag = value;
                OnPropertyChanged(nameof(AntalKuverterPerDag));
            }
        }



        /*Constructor*/
        public AfregningViewModel()
        {
            TimmeldListenMandag = new Model.TilmeldListe();
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
        }

        /*Methods*/
        public void BeregnKuverterMandag()
        {
            AntalKuverterPerDag = TimmeldListenMandag.getKuvurter();
        }


        public double KuverterPerBolig()
        {
           double Kuverter = 0.0;
            foreach (var i in TimmeldListenMandag)
            {
                if (i.HusNr == HentHusnummer)
                {
                    Kuverter = i.kuverterPerBolig();
                }

            }
            return Kuverter;
        }

        public int PrisPerBolig()
        {




            int pris = HentPris;
            return pris;
        }


        /*Hent data fra json til lister*/
        public async void HentDataFraDiskAsync()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(filnavn);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TimmeldListenMandag.Clear();
                TimmeldListenMandag.IndsetJson(jsonText);
                BeregnKuverterMandag();
                KuverterPerBolig();


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
