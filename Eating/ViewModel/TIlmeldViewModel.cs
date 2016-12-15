using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage;

namespace Eating.ViewModel
{
    public class TIlmeldViewModel : INotifyPropertyChanged
    {

        StorageFolder localfolder = null;
        private readonly string filnavn = "mandagJson.json";
        private readonly string filnavnTirsdag = "TirsdagJson.json";
        private readonly string filnavnOnsdag = "OnsdagJson.json";
        private readonly string filnavnTorsdag = "TorsdagJson.json";


       /* public double KuverterMandag;*/


        /*Properties*/
        public Model.Bolig NyBolig { get; set; }
        public Model.TilmeldListe TimmeldListenMandag { get; set; }
        public Model.TilmeldListe TimmeldListenTirsdag { get; set; }
        public Model.TilmeldListe TimmeldListenOnsdag { get; set; }
        public Model.TilmeldListe TimmeldListenTorsdag { get; set; }
        public AddCommand AddMandag { get; set; }
        public AddCommand AddTirsdag { get; set; }
        public AddCommand AddOnsdag { get; set; }
        public AddCommand AddTorsdag { get; set; }
        public RemoveMenuCommand RemoveFromMandagList { get; set; }
        public RemoveMenuCommand RemoveFromTirsdagList { get; set; }
        public RemoveMenuCommand RemoveFromOnsdagList { get; set; }
        public RemoveMenuCommand RemoveFromTorsdagList { get; set; }
        public PrisCommand beregnPris { get; set; }
      /*  public double _prisIalt;*/
        public event PropertyChangedEventHandler PropertyChanged;

        private Model.Bolig _selectedDagItem;

        public Model.Bolig SelectedDagItem
        {
            get { return _selectedDagItem; }
            set { _selectedDagItem = value;
                OnPropertyChanged(nameof(SelectedDagItem));
            }
        }

        /*summen*/
        public int testCount
        {
            get
            {
                /* return TimmeldListenMandag.Sum(p => p.NumberAdults);*/
                return TimmeldListenMandag[0].NumberAdults;
            }
        }

        /*
        public double IaltPris
        {
            get { return this._prisIalt; }
            set
            {
                this._prisIalt = value;
                OnPropertyChanged(nameof(IaltPris));

            }
        }
        */


        /*Constructor*/
        public TIlmeldViewModel()
        {
            
            _selectedDagItem = new Model.Bolig();
            TimmeldListenMandag = new Model.TilmeldListe();
            TimmeldListenTirsdag = new Model.TilmeldListe();
            TimmeldListenOnsdag = new Model.TilmeldListe();
            TimmeldListenTorsdag = new Model.TilmeldListe();
            NyBolig = new Model.Bolig();
            AddMandag = new AddCommand(AddDay);
            AddTirsdag = new AddCommand(AddDayTirsdag);
            AddOnsdag = new AddCommand(AddDayOnsdag);
            AddTorsdag = new AddCommand(AddDayTorsdag);
            RemoveFromMandagList = new RemoveMenuCommand(RemoveMandagListItem);
            RemoveFromTirsdagList = new RemoveMenuCommand(RemoveTirsdagListItem);
            RemoveFromOnsdagList = new RemoveMenuCommand(RemoveOnsdagListItem);
            RemoveFromTorsdagList = new RemoveMenuCommand(RemoveTorsdagListItem);
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
            HentDataFraDiskAsyncTirsdag();
            HentDataFraDiskAsyncOnsdag();
            HentDataFraDiskAsyncTorsdag();

           /* beregnPris = new PrisCommand(PrisPerHusstand);*/
        }




        /*Methodes */

        /*MANDAG*/
        public void AddDay()
        {
            var tempDay = new Model.Bolig();
            tempDay.HusNr = NyBolig.HusNr;
            tempDay.NumberAdults = NyBolig.NumberAdults;
            tempDay.NumberKidsZeroThree = NyBolig.NumberKidsZeroThree;
            tempDay.NumberKidsFourSix = NyBolig.NumberKidsFourSix;
            tempDay.NUmberKidsSevenFifteen = NyBolig.NUmberKidsSevenFifteen;
          
            TimmeldListenMandag.Add(tempDay);
            GemDataTilDiskAsync();

            OnPropertyChanged("testCount");
        }

        public  void RemoveMandagListItem()
        {
            TimmeldListenMandag.Remove(SelectedDagItem);
            GemDataTilDiskAsync();
            OnPropertyChanged("testCount");
        }

        //public async void jsonTest()
        //{
        //    StorageFile file = await localfolder.GetFileAsync(filnavn);
        //    string jsonText = await FileIO.ReadTextAsync(file);
        //    jsonText.Count();
        // }

        public async void GemDataTilDiskAsync()
        {
            string jsonText = this.TimmeldListenMandag.getJson();
            StorageFile file = await localfolder.CreateFileAsync(filnavn, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonText);
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
                OnPropertyChanged("testCount");
            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }
        }

        /*Tirsdag*/
        public void AddDayTirsdag()
        {
            var tempDay = new Model.Bolig();
            tempDay.HusNr = NyBolig.HusNr;
            tempDay.NumberAdults = NyBolig.NumberAdults;
            tempDay.NumberKidsZeroThree = NyBolig.NumberKidsZeroThree;
            tempDay.NumberKidsFourSix = NyBolig.NumberKidsFourSix;
            tempDay.NUmberKidsSevenFifteen = NyBolig.NUmberKidsSevenFifteen;
            TimmeldListenTirsdag.Add(tempDay);
            GemDataTilDiskAsyncTirsdag();
        }

        public void RemoveTirsdagListItem()
        {
            TimmeldListenTirsdag.Remove(SelectedDagItem);
            GemDataTilDiskAsyncTirsdag();
        }

        public async void GemDataTilDiskAsyncTirsdag()
        {
            string jsonText = this.TimmeldListenTirsdag.getJson();
            StorageFile file = await localfolder.CreateFileAsync(filnavnTirsdag, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonText);
        }



        public async void HentDataFraDiskAsyncTirsdag()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(filnavnTirsdag);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TimmeldListenTirsdag.Clear();
                TimmeldListenTirsdag.IndsetJson(jsonText);
            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }


        }

        /*Onsdag*/
        public void AddDayOnsdag()
        {
            var tempDay = new Model.Bolig();
            tempDay.HusNr = NyBolig.HusNr;
            tempDay.NumberAdults = NyBolig.NumberAdults;
            tempDay.NumberKidsZeroThree = NyBolig.NumberKidsZeroThree;
            tempDay.NumberKidsFourSix = NyBolig.NumberKidsFourSix;
            tempDay.NUmberKidsSevenFifteen = NyBolig.NUmberKidsSevenFifteen;
            TimmeldListenOnsdag.Add(tempDay);
            GemDataTilDiskAsyncOnsdag();
        }

        public void RemoveOnsdagListItem()
        {
            TimmeldListenOnsdag.Remove(SelectedDagItem);
            GemDataTilDiskAsyncOnsdag();
        }

        public async void GemDataTilDiskAsyncOnsdag()
        {
            string jsonText = this.TimmeldListenOnsdag.getJson();
            StorageFile file = await localfolder.CreateFileAsync(filnavnOnsdag, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonText);
        }

        public async void HentDataFraDiskAsyncOnsdag()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(filnavnOnsdag);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TimmeldListenOnsdag.Clear();
                TimmeldListenOnsdag.IndsetJson(jsonText);
            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }
        }


        /*Torsdag*/
        public void AddDayTorsdag()
        {
            var tempDay = new Model.Bolig();
            tempDay.HusNr = NyBolig.HusNr;
            tempDay.NumberAdults = NyBolig.NumberAdults;
            tempDay.NumberKidsZeroThree = NyBolig.NumberKidsZeroThree;
            tempDay.NumberKidsFourSix = NyBolig.NumberKidsFourSix;
            tempDay.NUmberKidsSevenFifteen = NyBolig.NUmberKidsSevenFifteen;
            TimmeldListenTorsdag.Add(tempDay);
            GemDataTilDiskAsyncTorsdag();
        }

        public void RemoveTorsdagListItem()
        {
            TimmeldListenTorsdag.Remove(SelectedDagItem);
            GemDataTilDiskAsyncTorsdag();
        }
        public async void GemDataTilDiskAsyncTorsdag()
        {
            string jsonText = this.TimmeldListenTorsdag.getJson();
            StorageFile file = await localfolder.CreateFileAsync(filnavnTorsdag, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonText);
        }

        public async void HentDataFraDiskAsyncTorsdag()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(filnavnTorsdag);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.TimmeldListenTorsdag.Clear();
                TimmeldListenTorsdag.IndsetJson(jsonText);
            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }


        }
        /*

        public void BeregnKuvert()
        {
            foreach(Model.Bolig beboere in TimmeldListenMandag)
            {
                KuverterMandag = KuverterMandag + (
                    (beboere.NumberAdults * beboere.KuvertProcentAdults) + 
                    (beboere.NumberKidsZeroThree * beboere.KuvertProcentZeroThree) +
                    (beboere.NumberKidsFourSix * beboere.KuvertProcentFourSIx) +
                    (beboere.NUmberKidsSevenFifteen * beboere.KuvertProcentSevenFifteen)
                    );
            }
        }


        public void PrisPerHusstand()
        {
            BeregnKuvert();

            foreach (Model.Bolig beboere in TimmeldListenMandag)
            {
                
                beboere.PrisPerFamile = (_prisIalt / KuverterMandag) *
                     ((beboere.NumberAdults * beboere.KuvertProcentAdults) +
                    (beboere.NumberKidsZeroThree * beboere.KuvertProcentZeroThree) +
                    (beboere.NumberKidsFourSix * beboere.KuvertProcentFourSIx) +
                    (beboere.NUmberKidsSevenFifteen * beboere.KuvertProcentSevenFifteen)
                    );
            }
            GemDataTilDiskAsync();
            
        }
        */

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
