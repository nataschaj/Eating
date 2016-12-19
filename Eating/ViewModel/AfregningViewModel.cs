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
        private readonly string Mandag = "mandagJson.json";
        private readonly string Tirsdag = "TirsdagJson.json";
        private readonly string Onsdag = "OnsdagJson.json";
        private readonly string Torsdag = "TorsdagJson.json";
        private readonly string Menu = "jsonMENU.json";
        public event PropertyChangedEventHandler PropertyChanged;

        /*Uge lister*/
        public Model.TilmeldListe TimmeldListenMandag { get; set; }
        public Model.TilmeldListe TilmeldListenTirsdag { get; set; }
        public Model.TilmeldListe TilmeldListenOnsdag { get; set; }
        public Model.TilmeldListe TilmeldListenTorsdag { get; set; }
        public Model.PlanlaegListe MenuListe { get; set; }

        private int hentHusnummer;
        public int HentHusnummer
        {
            get { return hentHusnummer; }
            set { hentHusnummer = value; OnPropertyChanged(nameof(HentHusnummer)); OnPropertyChanged(nameof(hentKuverter)); OnPropertyChanged(nameof(hentBoligPrisPerUge));
            }
        }



        public double hentKuverter { get {
                return KuverterPerBoligIalt();
            }
        }

        public double hentBoligPrisPerUge { get {
                return PrisPerBoligForUgen();
            } }

        private int hetPris;
        public int HentPris
        {
            get { return hetPris; }
            set { hetPris = value; OnPropertyChanged(nameof(HentPris)); OnPropertyChanged(nameof(visPris)); }
        }

        public int visPris { get {
                return PrisPerBolig();
            } }


        /*Mandag kuverter*/
        private double antalKuverterPerUge;
        public double AntalKuverterPerUge
        {
            get { return antalKuverterPerUge; }
            set { antalKuverterPerUge = value;
                OnPropertyChanged(nameof(AntalKuverterPerUge));
            }
        }


        /*PRIS*/
        private double antalPrisPerUge;

        public double AntalPrisPerUge
        {
            get { return antalPrisPerUge; }
            set { antalPrisPerUge = value;
                OnPropertyChanged(nameof(AntalPrisPerUge));
            }
        }

        /*PRIS FOR KUVERTER PER UGE*/
        private double prisPerKuvert;

        public double PrisPerKuvert
        {
            get { return prisPerKuvert; }
            set { prisPerKuvert = value;
                OnPropertyChanged(nameof(PrisPerKuvert));
            }
        }




        /*Constructor*/
        public AfregningViewModel()
        {
           
            TimmeldListenMandag = new Model.TilmeldListe();
            TilmeldListenTirsdag = new Model.TilmeldListe();
            TilmeldListenOnsdag = new Model.TilmeldListe();
            TilmeldListenTorsdag = new Model.TilmeldListe();
            MenuListe = new Model.PlanlaegListe();
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
            HentDataFraDiskAsyncTirsdag();
            HentDataFraDiskAsyncOnsdag();
            HentDataFraDiskAsyncTorsdag();

            HentDataFraDiskAsyncMenu();
          


        }

        /*Methods*/
        public double BeregnKuverterForUgen()
        {
            return AntalKuverterPerUge = TimmeldListenMandag.getKuvurter() + TilmeldListenTirsdag.getKuvurter() + TilmeldListenOnsdag.getKuvurter() + TilmeldListenTorsdag.getKuvurter();
        }


        public double BeregnPrisForUgen()
        {
           return  AntalPrisPerUge = MenuListe.getPriser();
        }

        public double BeregnKuvertPrisPerUge()
        {
            return PrisPerKuvert = (BeregnPrisForUgen() / BeregnKuverterForUgen());
        }



        /*Kuverter MANDAG*/
        public double KuverterPerBoligMandag()
        {
           double Kuverter = 0.0;
            foreach (var i in TimmeldListenMandag )
            {
                if (i.HusNr == HentHusnummer)
                {
                    Kuverter = i.kuverterPerBolig();
                }

            }
            return Kuverter;
        }


        /*Kuverter Tirsdag*/
        public double KuverterPerBoligTirsdag()
        {
            double Kuverter = 0.0;
            foreach (var i in TilmeldListenTirsdag)
            {
                if (i.HusNr == HentHusnummer)
                {
                    Kuverter = i.kuverterPerBolig();
                }

            }
            return Kuverter;
        }

        /*Kuverter Onsdag*/
        public double KuverterPerBoligOnsdag()
        {
            double Kuverter = 0.0;
            foreach (var i in TilmeldListenOnsdag)
            {
                if (i.HusNr == HentHusnummer)
                {
                    Kuverter = i.kuverterPerBolig();
                }

            }
            return Kuverter;
        }

        /*Kuverter Torsdag*/
        public double KuverterPerBoligTorsdag()
        {
            double Kuverter = 0.0;
            foreach (var i in TilmeldListenTorsdag)
            {
                if (i.HusNr == HentHusnummer)
                {
                    Kuverter = i.kuverterPerBolig();
                }

            }
            return Kuverter;
        }


        public double KuverterPerBoligIalt()
        {
            return KuverterPerBoligMandag() + KuverterPerBoligTirsdag() + KuverterPerBoligOnsdag() + KuverterPerBoligTorsdag();
        }

        public int PrisPerBolig()
        {
            int pris = HentPris;
            return pris;
        }


        public double PrisPerBoligForUgen()
        {
            return KuverterPerBoligIalt() * BeregnKuvertPrisPerUge();
        }

        /*Mandag*/
        /*Hent data fra json til lister*/
        public async void HentDataFraDiskAsync()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(Mandag);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TimmeldListenMandag.Clear();
                TimmeldListenMandag.IndsetJson(jsonText);
                BeregnKuverterForUgen();

                BeregnKuvertPrisPerUge();

            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }
        }


        /*Tirsdag*/
        /*Hent data fra json til lister*/
        public async void HentDataFraDiskAsyncTirsdag()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(Tirsdag);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TilmeldListenTirsdag.Clear();
                TilmeldListenTirsdag.IndsetJson(jsonText);
                BeregnKuverterForUgen();

                BeregnKuvertPrisPerUge();

            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }
        }


        /*Onsdag*/
        /*Hent data fra json til lister*/
        public async void HentDataFraDiskAsyncOnsdag()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(Onsdag);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TilmeldListenOnsdag.Clear();
                TilmeldListenOnsdag.IndsetJson(jsonText);
                BeregnKuverterForUgen();


                BeregnKuvertPrisPerUge();

            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }
        }


        /*Torsdag*/
        /*Hent data fra json til lister*/
        public async void HentDataFraDiskAsyncTorsdag()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(Torsdag);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TilmeldListenTorsdag.Clear();
                TilmeldListenTorsdag.IndsetJson(jsonText);
                BeregnKuverterForUgen();

                BeregnKuvertPrisPerUge();
            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }
        }



        /*MENU*/
        /*Hent data fra json til lister*/
        public async void HentDataFraDiskAsyncMenu()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(Menu);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.MenuListe.Clear();
                MenuListe.IndsetJson(jsonText);
                //BeregnKuverterForUgen();

                BeregnKuvertPrisPerUge();
               
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
