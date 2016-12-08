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
        private readonly string filnavn = "JsonMENU.json"; //test

        /*Properties*/
        public Model.Planlaeg nyPlanlaeg { get; set; }
        public Model.PlanlaegListe PlanlaegListe { get; set; }
        public AddCommand AddMenu { get; set; }
        public saveCommand SaveMenu { get;  set; }
        public hentCommand HentMenu { get; set; }
        public RemoveMenuCommand RemoveMenuCommand { get; set; }
        public List<String> UgeDage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Model.Planlaeg _selectedMenu;

        public Model.Planlaeg SelectedMenu
        {
            get { return _selectedMenu; }
            set { _selectedMenu = value; }
        }

        

       

        /*Constructor*/
        public MainViewModel()
        {
            _selectedMenu = new Model.Planlaeg();
            nyPlanlaeg = new Model.Planlaeg();
            PlanlaegListe = new Model.PlanlaegListe();
            AddMenu = new AddCommand(AddNewMenu);
            RemoveMenuCommand = new RemoveMenuCommand(RemoveThisMenu);
            SaveMenu = new saveCommand(GemDataTilDiskAsync);
            HentMenu = new hentCommand(HentDataFraDiskAsync);
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
            UgeDage = new List<String>();
            UgeDage.Add("Mandag");
            UgeDage.Add("Tirsdag");
            UgeDage.Add("Onsdag");
            UgeDage.Add("Torsdag");
        }

        /*Methodes */
        public void AddNewMenu()
        {
            var temp = new Model.Planlaeg();
            temp.Ret = nyPlanlaeg.Ret;
            temp.ChefKok = nyPlanlaeg.ChefKok;
            temp.Hjaelpere = nyPlanlaeg.Hjaelpere;
            temp.Oprydere = nyPlanlaeg.Oprydere;
            temp.Dag = nyPlanlaeg.Dag;
            temp.OpretDato = DateTime.Now;
            PlanlaegListe.Add(temp);
            GemDataTilDiskAsync();
        }

        public void RemoveThisMenu()
        {
            PlanlaegListe.Remove(SelectedMenu);
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
