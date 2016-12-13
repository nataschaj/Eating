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

        public event PropertyChangedEventHandler PropertyChanged;

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


        /*Constructor*/
        public TIlmeldViewModel()
        {
            TimmeldListenMandag = new Model.TilmeldListe();
            TimmeldListenTirsdag = new Model.TilmeldListe();
            TimmeldListenOnsdag = new Model.TilmeldListe();
            TimmeldListenTorsdag = new Model.TilmeldListe();
            NyBolig = new Model.Bolig();
            AddMandag = new AddCommand(AddDay);
            AddTirsdag = new AddCommand(AddDayTirsdag);
            AddOnsdag = new AddCommand(AddDayOnsdag);
            AddTorsdag = new AddCommand(AddDayTorsdag);
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
            HentDataFraDiskAsyncTirsdag();
            HentDataFraDiskAsyncOnsdag();
            HentDataFraDiskAsyncTorsdag();
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
        }


       
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

    }
}
