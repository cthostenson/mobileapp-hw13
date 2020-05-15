using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MarvelDemo.Models;
using MarvelDemo.Services;
using Xamarin.Forms;

namespace MarvelDemo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly IMarvelDataService _dataService;

        ObservableCollection<Character> _characters;
        public ObservableCollection<Character> Characters
        {
            get { return _characters; }
            set
            {
                _characters = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
        }

        public void Init()
        {
            LoadCharactersCommand.Execute(null);
            //LoadCharacters();
        }

        ICommand _loadCharactersCommand;
        public ICommand LoadCharactersCommand
        {
            get
            {
                return _loadCharactersCommand ?? (_loadCharactersCommand = new Command(async () => await LoadAllCharacters()));
            }
        }

        async Task LoadAllCharacters()
        {
            Characters = new ObservableCollection<Character>();

            
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Characters.Clear();

                var allCharacters = await _dataService.GetAllCharacters();

                foreach (Character c in allCharacters)
                    Characters.Add(c);
            }
            finally
            {
                IsBusy = false;
            }

        }


        public void LoadCharacters()
        {
            //Characters = new ObservableCollection<Character>();

            /*
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Characters.Clear();

                var allCharacters = await _dataService.GetAllCharacters();

                foreach (Character c in allCharacters)
                    Characters.Add(c);
            }
            finally
            {
                IsBusy = false;
            }*/

            //Characters.Add(new Character { Name = _dataService.GetAllCharacters().ToString() });

            //Characters.Add(new Character
            //{

            //}); 

            // Hard-coded list of Characters instead of pulling from the API so we can get just the specific Avengers we want to display:
           /* Characters.Add(new Character
            {
                Id = 1009368,
                Name = "Iron Man",
                IconPath = "ironman_52.png",
                SeriesId = 2029,
                Thumbnail =
                    new Image {Path = "http://i.annihil.us/u/prod/marvel/i/mg/9/c0/527bb7b37ff55/standard_medium", Extension = "jpg"}
            });
            Characters.Add(new Character
            {
                Id = 1009220,
                Name = "Captain America",
                IconPath = "captainamerica_52.png",
                SeriesId = 1996,
                Thumbnail =
                    new Image {Path = "http://i.annihil.us/u/prod/marvel/i/mg/3/50/537ba56d31087/standard_medium", Extension = "jpg"}
            });
            Characters.Add(new Character
            {
                Id = 1009664,
                Name = "Thor",
                IconPath = "thor_52.png",
                SeriesId = 2083,
                Thumbnail =
                    new Image {Path = "http://i.annihil.us/u/prod/marvel/i/mg/d/d0/5269657a74350/standard_medium", Extension = "jpg"}
            });*/
        }
    }
}
