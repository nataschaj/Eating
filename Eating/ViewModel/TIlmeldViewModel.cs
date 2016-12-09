using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage;
using Windows.UI.Popups;

namespace Eating.ViewModel
{
    public class TIlmeldViewModel : INotifyPropertyChanged
    {

        StorageFolder localfolder = null;
        private readonly string filnavn = "JsonMandag.json"; //test

        public string mandag { get; set; }
        public string tirsdag { get; set; }
        public string onsdag { get; set; }
        public string torsdag { get; set; }
        public AddCommand AddBolig { get; set; }
        public saveCommand SaveBolig { get; set; }
        public hentCommand HentBolig { get; set; }
        public RemoveMenuCommand RemoveBoligCommand { get; set; }
        public List<String> UgeDageBolig { get; set; }
        public Model.Bolig nyBolig { get; set; }
        public Model.TilmeldListe TilmeldListe { get; set; }


        public TIlmeldViewModel()
        {
            this.mandag = "Mandag";
            _selectedBolig = new Model.Bolig();
            nyBolig = new Model.Bolig();
            TilmeldListe = new Model.TilmeldListe();
            AddBolig = new AddCommand(AddNewBolig);
            RemoveBoligCommand = new RemoveMenuCommand(RemoveThisBolig);
            SaveBolig = new saveCommand(GemDataTilDiskAsync);
            HentBolig = new hentCommand(HentDataFraDiskAsync);
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
            UgeDageBolig = new List<String>();
            UgeDageBolig.Add(mandag);
        }

        public void AddNewBolig()
        {
            var tempBolig = new Model.Bolig();
            tempBolig.HusNr = nyBolig.HusNr;
            tempBolig.NumberAdults = nyBolig.NumberAdults;
            tempBolig.NumberKidsZeroThree = nyBolig.NumberKidsFourSix;
            tempBolig.NUmberKidsSevenFifteen = nyBolig.NUmberKidsSevenFifteen;
            TilmeldListe.Add(tempBolig);
            GemDataTilDiskAsync();
        }


        private Model.Bolig _selectedBolig;

        public Model.Bolig SelectedBolig
        {
            get { return _selectedBolig; }
            set
            {
                _selectedBolig = value;
                OnPropertyChanged(nameof(SelectedBolig));
            }
        }

        public void RemoveThisBolig()
        {
           TilmeldListe.Remove(SelectedBolig);
            GemDataTilDiskAsync();
        }

        public async void GemDataTilDiskAsync()
        {
            string jsonText = this.TilmeldListe.getJson();
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
                this.TilmeldListe.Clear();
                TilmeldListe.IndsetJson(jsonText);
            }
            catch (Exception)
            {/*
                 MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); 
                  */
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

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
