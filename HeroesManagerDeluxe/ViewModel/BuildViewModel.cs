using HeroesDataAccessLayer;
using HeroesDomainModel;
using HeroesManagerDeluxe.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe.ViewModel
{
    public class BuildViewModel : WorkspaceViewModel
    {
        private Build build;
        private BuildDAO bDAO;
        public ObservableCollection<TalentViewModel> SelectedTalents { get; private set; }
        public CommandViewModel SaveCommand { get; private set; }

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="build">Build Entity Object</param>
        public BuildViewModel(Build build, BuildDAO bDAO)
        {
            this.build = build;
            LoadTalents();
            Setup(bDAO);
        }

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="talents">List of selected talents, List of TalentViewModel expected</param>
        public BuildViewModel(IList talents, BuildDAO bDAO)
        {
            SelectedTalents = new ObservableCollection<TalentViewModel>(talents.Cast<TalentViewModel>().ToList());
            build = new Build();
            build.hero_id = SelectedTalents[0].HeroId;
            Setup(bDAO);
        }

        /// <summary>
        /// Method calls CreateCommands and sets header for tab card
        /// </summary>
        private void Setup(BuildDAO bDAO)
        {
            //base.DisplayName = Hero + " " + Resources.Name_BuildViewModel;
            this.bDAO = bDAO;
            CreateCommands();
        }

        /// <summary>
        /// Populates list of talents with TalentViewModels
        /// </summary>
        private void LoadTalents()
        {
            List<TalentViewModel> all = (from talent in build.Talent
                                         select new TalentViewModel(talent)).ToList();
            SelectedTalents = new ObservableCollection<TalentViewModel>(all);
        }

        /// <summary>
        /// Creates commands used in Build View
        /// </summary>
        private void CreateCommands()
        {
            SaveCommand = new CommandViewModel(Resources.SaveBuild,
                new RelayCommand(param => SaveBuild()));
        }

        /// <summary>
        /// Saves or Modifies build entity object into data base
        /// </summary>
        private void SaveBuild()
        {
            if(build.id == 0)
            {
                foreach (var item in SelectedTalents)
                {
                    build.Talent.Add(item.ReturnTalent());
                }
                bDAO.Insert(build);
            }
            else
            {
                bDAO.Update(build);
            }
            bDAO.Save();
        }

        public string Name
        {
            get
            {
                return build.name;
            }
            set
            {
                if(value != build.name)
                {
                    build.name = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return build.description;
            }
            set
            {
                build.description = value;
            }
        }

        //public string Hero
        //{
        //    get
        //    {
        //        return build.Hero.name;
        //    }
        //}
    }
}
