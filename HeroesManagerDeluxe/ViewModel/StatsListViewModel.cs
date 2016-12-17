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
        //Next login screen I think it should be a dialog, now I know how to use dialogs with MVVM
        //Dialog should initialize DB for sign in user, i.e it should create table for every hero in Stats
        //if user is the first one, it should initialize Average_Stats too.
        public ObservableCollection<StatsViewModel> Stats { get; private set; }
        public List<string> FilesPath { get; set; }
        public StatsViewModel SelectedHeroStatistics { get; set; }
        public CommandViewModel ShowDetailCommand { get; private set; }
        public CommandViewModel OpenFileCommand { get; private set; }
        private DialogService service;
        private StatsDAO sDAO;
        private AverageStatsDAO asDAO;
        private UserViewModel user;
        private string pathToReplays;
        private ReplayProcessing replayProcessing;
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

        public StatsListViewModel(StatsDAO sDAO, AverageStatsDAO asDAO, UserViewModel user)
        {
            this.sDAO = sDAO;
            this.asDAO = asDAO;
            this.user = user;
            replayProcessing = new ReplayProcessing();
            LoadStats();
            CreateCommands();
        }

        public void LoadStats()
        {
            List<StatsViewModel> all = (from stats in sDAO.GetAll()
                                        select new StatsViewModel(stats, asDAO.GetStatsForSpecifiedHero(stats.hero_id))).ToList();
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

        public async Task LoadAndCalculateStatistics()
        {
            
            foreach (var path in FilesPath)
            {
                await Task.Run(() => ProcessData(path));
            }
        }

        private void ProcessData(string path)
        {
            replayProcessing.LoadReplay(path);
            ProcessDataForPlayer();
            ProcessDataForWorld();
        }

        private void ProcessDataForPlayer()
        {
            var heroReplay = replayProcessing.Replay.Players.FirstOrDefault(p => p.Name == user.Name);
            var tmpStat = Stats.First(s => s.Hero == heroReplay.Character);
            CalculateWorldAverage(heroReplay.ScoreResult, tmpStat.AverageStats);

        }

        private void ProcessDataForWorld()
        {
            foreach (var player in replayProcessing.Replay.Players)
            {
                var tmpStat = Stats.First(s => s.Hero == player.Character);
                CalculateWorldAverage(player.ScoreResult, tmpStat.AverageStats);
            }
        }

        private void Calculate(Heroes.ReplayParser.ScoreResult replayStats, StatsViewModel heroStats)
        {
            //if(heroStats == null)
            {
                //TODO at first run create all average stats and average player stats and fill it with 0s
                //then for every sign in create only all average player stats and fill it with 0s
                //so at this point there is no possibility that something is not existing in DB
                //tl;dr no need for testing that, remove after addind initialize
            }
            heroStats.GamesCount++;
            heroStats.PlayerAverageDamageTaken = replayProcessing.CalculateAverage(heroStats.PlayerAverageDamageTaken, replayStats.DamageTaken, heroStats.GamesCount);
            heroStats.PlayerAverageHealingDone = replayProcessing.CalculateAverage(heroStats.PlayerAverageHealingDone, replayStats.Healing, heroStats.GamesCount);
            heroStats.PlayerAverageHeroDamage = replayProcessing.CalculateAverage(heroStats.PlayerAverageHeroDamage, replayStats.HeroDamage, heroStats.GamesCount);
            heroStats.PlayerAverageSiegeDamage = replayProcessing.CalculateAverage(heroStats.PlayerAverageSiegeDamage, replayStats.SiegeDamage, heroStats.GamesCount);
            heroStats.PlayerAverageDeaths = replayProcessing.CalculateAverage(heroStats.PlayerAverageDeaths, replayStats.Deaths, heroStats.GamesCount);
            heroStats.PlayerAverageKills = replayProcessing.CalculateAverage(heroStats.PlayerAverageKills, replayStats.SoloKills, heroStats.GamesCount);
            heroStats.PlayerAverageTakedowns = replayProcessing.CalculateAverage(heroStats.PlayerAverageTakedowns, replayStats.Takedowns, heroStats.GamesCount);
            heroStats.PlayerAverageXP = replayProcessing.CalculateAverage(heroStats.PlayerAverageXP, replayStats.ExperienceContribution, heroStats.GamesCount);

            if (heroStats.HigestKillStreak < replayStats.HighestKillStreak)
            {
                heroStats.HigestKillStreak = replayStats.HighestKillStreak;
            }
            if (replayStats.MatchAwards.Count != 0)
            {
                heroStats.TimesAwarded++;
            }
            if (replayStats.MatchAwards.Contains(Heroes.ReplayParser.MatchAwardType.MVP))
            {
                heroStats.TimesMVP++;
            }
        }

        private void CalculateWorldAverage(Heroes.ReplayParser.ScoreResult replayStats, AverageStatsViewModel averageStats)
        {
            averageStats.AverageDamageTaken = replayProcessing.CalculateAverage(averageStats.AverageDamageTaken, replayStats.DamageTaken, averageStats.GamesCount);
            averageStats.AverageHealingDone = replayProcessing.CalculateAverage(averageStats.AverageHealingDone, replayStats.Healing, averageStats.GamesCount);
            averageStats.AverageHeroDamage = replayProcessing.CalculateAverage(averageStats.AverageHeroDamage, replayStats.HeroDamage, averageStats.GamesCount);
            averageStats.AverageSiegeDamage = replayProcessing.CalculateAverage(averageStats.AverageSiegeDamage, replayStats.SiegeDamage, averageStats.GamesCount);
            averageStats.AverageDeaths = replayProcessing.CalculateAverage(averageStats.AverageDeaths, replayStats.Deaths, averageStats.GamesCount);
            averageStats.AverageKills = replayProcessing.CalculateAverage(averageStats.AverageKills, replayStats.SoloKills, averageStats.GamesCount);
            averageStats.AverageTakedowns = replayProcessing.CalculateAverage(averageStats.AverageTakedowns, replayStats.Takedowns, averageStats.GamesCount);
            averageStats.AverageXP = replayProcessing.CalculateAverage(averageStats.AverageXP, replayStats.ExperienceContribution, averageStats.GamesCount);

            if (averageStats.HigestKillStreak < replayStats.HighestKillStreak)
            {
                averageStats.HigestKillStreak = replayStats.HighestKillStreak;
            }
            if (replayStats.MatchAwards.Count != 0)
            {
                averageStats.TimesAwarded++;
            }
            if (replayStats.MatchAwards.Contains(Heroes.ReplayParser.MatchAwardType.MVP))
            {
                averageStats.TimesMVP++;
            }
        }
    }
}
