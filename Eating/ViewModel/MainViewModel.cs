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
    class MainViewModel : INotifyPropertyChanged
    {

        StorageFolder localfolder = null;
        private readonly string filnavn = "JsonMENU.json";

        /*Properties*/
        public Model.Planlaeg nyPlanlaeg { get; set; }
        public Model.PlanlaegListe PlanlaegListe { get; set; }
        public AddCommand AddMenu { get; set; }
        public saveCommand SaveMenu { get;  set; }
        public hentCommand HentMenu { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        

        /*Constructor*/
        public MainViewModel()
        {
            nyPlanlaeg = new Model.Planlaeg();
            PlanlaegListe = new Model.PlanlaegListe();
            AddMenu = new AddCommand(AddNewMenu);
            SaveMenu = new saveCommand(GemDataTilDiskAsync);
            HentMenu = new hentCommand(HentDataFraDiskAsync);
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();

        }

        /*Methodes */
        public void AddNewMenu()
        {
            var temp = new Model.Planlaeg();
            temp.Ret = nyPlanlaeg.Ret;
            temp.ChefKok = nyPlanlaeg.ChefKok;
            temp.Hjaelpere = nyPlanlaeg.Hjaelpere;
            temp.Oprydere = nyPlanlaeg.Oprydere;
            PlanlaegListe.Add(temp);
            GemDataTilDiskAsync();
        }

        public async void GemDataTilDiskAsync()
        {
            string jsonText = this.PlanlaegListe.getJson();
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
                this.PlanlaegListe.Clear();
                PlanlaegListe.IndsetJson(jsonText);
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
