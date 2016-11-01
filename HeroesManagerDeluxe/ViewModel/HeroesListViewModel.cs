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
        public ObservableCollection<HeroDetailsViewModel> Heroes { get; private set; }
        public HeroDetailsViewModel SelectedHero { get; set; }
        public CollectionView HeroesCollectionView { get; private set; }
        public string SearchBar { get; set; }
        public CommandViewModel SearchCommand { get; private set; }
        public CommandViewModel ShowDetailCommand { get; private set; }
        private readonly HeroesDAO hDAO;

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="hDAO">Hero Data Access Object</param>
        public HeroesListViewModel(HeroesDAO hDAO)
        {
            this.hDAO = hDAO;
            base.DisplayName = Resources.Name_HeroesListViewModel;
            LoadHeroes();

            SearchCommand = new CommandViewModel(Resources.SearchCommand,
                new RelayCommand(param => UpdateView()));
            ShowDetailCommand = new CommandViewModel(Resources.HeroDetailCommand,
                new RelayCommand(param => ShowDetail()));
        }

        /// <summary>
        /// Loads list from database and sets filter to collection
        /// </summary>
        private void LoadHeroes()
        {
            List<HeroDetailsViewModel> all = (from hero in hDAO.GetAll()
                                                  select new HeroDetailsViewModel(hero, hDAO)).ToList();
            Heroes = new ObservableCollection<HeroDetailsViewModel>(all);
            HeroesCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(Heroes);
            HeroesCollectionView.Filter = HeroesFilter;
        }

        /// <summary>
        /// Send message via Galasoft.Messenger informing MainViewModel that user clicked
        /// Details button i.e. MainViewModel have to display new workspace.
        /// </summary>
        private void ShowDetail()
        {
            DisplayWorkspaceMessage message = new DisplayWorkspaceMessage(SelectedHero);
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
