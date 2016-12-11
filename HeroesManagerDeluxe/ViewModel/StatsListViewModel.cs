using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesDataAccessLayer;
using HeroesManagerDeluxe.Properties;
using GalaSoft.MvvmLight.Messaging;

namespace HeroesManagerDeluxe.ViewModel
{
    public class StatsListViewModel : WorkspaceViewModel
    {
        //TODO stats list view
        public ObservableCollection<StatsViewModel> Stats { get; private set; }
        public List<string> FilesPath { get; set; }
        public StatsViewModel SelectedHeroStatistics { get; set; }
        public CommandViewModel ShowDetailCommand { get; private set; }
        public CommandViewModel OpenFileCommand { get; private set; }
        private DialogService service;
        private StatsDAO sDAO;
        private AverageStatsDAO asDAO;
        private string pathToReplays;

        public string PathToReplays
        {
            get
            {
                return pathToReplays;
            }
            set
            {
                if(value != pathToReplays)
                {
                    pathToReplays = value;

                    OnPropertyChanged();
                }
            }
        }
        public StatsListViewModel(StatsDAO sDAO, AverageStatsDAO asDAO)
        {
            this.sDAO = sDAO;
            this.asDAO = asDAO;
            LoadStats();
            CreateCommands();
        }

        public void LoadStats()
        {
            List<StatsViewModel> all = (from stats in sDAO.GetAll()
                                              select new StatsViewModel(stats, asDAO.GetById(stats.hero_id))).ToList();
            Stats = new ObservableCollection<StatsViewModel>(all);
        }

        private void CreateCommands()
        {
            ShowDetailCommand = new CommandViewModel(Resources.Command_StatsDetail,
                new RelayCommand(param => ShowDetails()));
            OpenFileCommand = new CommandViewModel(Resources.Command_Dialog,
                new RelayCommand(param => Messenger.Default.Send(service)));
            service = new DialogService
            {
                FilePicked = OpenFile
            };
        }

        private void ShowDetails()
        {
            SendMessage(SelectedHeroStatistics);
        }

        private void OpenFile()
        {
            FilesPath = service.PickedFiles;
            PathToReplays = string.Join(";", FilesPath);
        }

        /// <summary>
        /// Send message via Galasoft.Messenger informing MainViewModel that user clicked
        /// button i.e. MainViewModel have to display new workspace.
        /// </summary>
        /// <param name="workspace">Workspace do be displayed</param>
        private void SendMessage(WorkspaceViewModel workspace)
        {
            DisplayWorkspaceMessage message = new DisplayWorkspaceMessage(workspace);
            Messenger.Default.Send(message);
        }
    }
}
