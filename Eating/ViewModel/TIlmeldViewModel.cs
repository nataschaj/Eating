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

        public event PropertyChangedEventHandler PropertyChanged;

        /*Properties*/
        public Model.Bolig NyBolig { get; set; }
        public Model.TilmeldListe TimmeldListenMandag { get; set; }
        public Model.TilmeldListe TimmeldListenTirsdag { get; set; }
        public AddCommand AddMandag { get; set; }
        public AddCommand AddTirsdag { get; set; }


        /*Constructor*/
        public TIlmeldViewModel()
        {
            TimmeldListenMandag = new Model.TilmeldListe();
            TimmeldListenTirsdag = new Model.TilmeldListe();
            NyBolig = new Model.Bolig();
            AddMandag = new AddCommand(AddDay);
            AddTirsdag = new AddCommand(AddDayTirsdag);
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
        }

  


        /*Methodes */
        public void AddDayTirsdag()
        {
            var tempDay = new Model.Bolig();
            tempDay.HusNr = NyBolig.HusNr;
            tempDay.NumberAdults = NyBolig.NumberAdults;
            tempDay.NumberKidsZeroThree = NyBolig.NumberKidsZeroThree;
            tempDay.NumberKidsFourSix = NyBolig.NumberKidsFourSix;
            tempDay.NUmberKidsSevenFifteen = NyBolig.NUmberKidsSevenFifteen;
            TimmeldListenTirsdag.Add(tempDay);
        
        }



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


        /*MANDAG*/
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
    }
}
