using GalaSoft.MvvmLight.Messaging;
using HeroesDataAccessLayer;
using HeroesDomainModel;
using HeroesManagerDeluxe.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HeroesManagerDeluxe.ViewModel
{
    public class HeroesListViewModel : WorkspaceViewModel
    {
        private readonly HeroesDAO hDAO;
        private readonly BuildDAO bDAO;
        public ObservableCollection<HeroDetailsViewModel> Heroes { get; private set; }
        //public ObservableCollection<BuildViewModel> Builds { get; private set; }
        public CollectionView HeroesCollectionView { get; private set; }
        public string SearchBar { get; set; }
        public CommandViewModel SearchCommand { get; private set; }
        public CommandViewModel ShowDetailCommand { get; private set; }
        public CommandViewModel ShowBuildCommand { get; private set; }
        public BuildViewModel SelectedBuild { get; set; }
        private HeroDetailsViewModel selectedHero;
        public HeroDetailsViewModel SelectedHero
        {
            get
            {
                return selectedHero;
            }
            set
            {
                if(value != selectedHero)
                {
                    selectedHero = value;

                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="hDAO">Hero Data Access Object</param>
        public HeroesListViewModel(HeroesDAO hDAO, BuildDAO bDAO)
        {
            this.hDAO = hDAO;
            this.bDAO = bDAO;
            base.DisplayName = Resources.Name_HeroesListViewModel;
            LoadHeroes();

            CreateCommands();
            SelectedHero = Heroes.First();
        }

        private void CreateCommands()
        {
            SearchCommand = new CommandViewModel(Resources.Command_Search,
                new RelayCommand(param => UpdateView()));
            ShowDetailCommand = new CommandViewModel(Resources.Command_HeroDetail,
                new RelayCommand(param => ShowDetail()));
            ShowBuildCommand = new CommandViewModel(Resources.Command_BuildDetail,
                new RelayCommand(param => ShowBuildDetail()));
        }

        /// <summary>
        /// Loads list from database and sets filter to collection
        /// </summary>
        private void LoadHeroes()
        {
            List<HeroDetailsViewModel> all = (from hero in hDAO.GetAll()
                                                  select new HeroDetailsViewModel(hero, hDAO, bDAO)).ToList();
            Heroes = new ObservableCollection<HeroDetailsViewModel>(all);
            HeroesCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(Heroes);
            HeroesCollectionView.Filter = HeroesFilter;
        }

        /// <summary>
        /// Show details of selected hero
        /// </summary>
        private void ShowDetail()
        {
            SelectedHero.LoadAbilities();
            SelectedHero.LoadTalents();
            SendMessage(SelectedHero);
        }

        /// <summary>
        /// Show selected build
        /// </summary>
        private void ShowBuildDetail()
        {
            SendMessage(SelectedBuild);
        }

        /// <summary>
        /// Send message via Galasoft.Messenger informing MainViewModel that user clicked
        /// button i.e. MainViewModel have to display new workspace.
        /// </summary>
        /// <param name="workspace">Workspace do be displayed</param>
        private void SendMessage(WorkspaceViewModel workspace)
        {
            DisplayWorkspaceMessage message = new DisplayWorkspaceMessage(workspace);
            Messenger.Default.Send<DisplayWorkspaceMessage>(message);
        }

        /// <summary>
        /// Filter for CollectionView of Emails,
        /// compares text in SearchBar field with hero's name. Case insensitive.
        /// </summary>
        private bool HeroesFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchBar))
            {
                return true;
            }
            return ((obj as HeroDetailsViewModel).Name.IndexOf(SearchBar, StringComparison.OrdinalIgnoreCase) >= 0);
            //return ((obj as EmailViewModel).Email.Contains(SearchBar)); //case sensitive
        }

        /// <summary>
        /// Refreshes CollectionView
        /// </summary>
        public void UpdateView()
        {
            HeroesCollectionView.Refresh();
        }
    }
}
