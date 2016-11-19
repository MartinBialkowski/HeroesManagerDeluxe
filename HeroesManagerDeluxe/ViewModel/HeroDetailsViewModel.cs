using GalaSoft.MvvmLight.Messaging;
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
    //TODO 1 Tab for 1 hero, 
    public class HeroDetailsViewModel : WorkspaceViewModel
    {
        private Hero hero;
        private readonly BuildDAO bDAO;
        //private readonly HeroesDAO hDAO;
        public ObservableCollection<AbilityViewModel> Abilities { get; private set; }
        public ObservableCollection<TalentViewModel> Talents { get; private set; }
        public BuildViewModel Build { get; private set; }
        public CommandViewModel UpdateCommand { get; private set; }
        public CommandViewModel CreateBuildCommand { get; private set; }
        public IList SelectedTalents { get; set; }

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="hero">Entity object</param>
        /// <param name="hDAO">Data Access Object</param>
        /// <param name="bDAO">Data Access Object</param>
        public HeroDetailsViewModel(Hero hero, HeroesDAO hDAO, BuildDAO bDAO)
        {
            this.hero = hero;
            //this.hDAO = hDAO;
            this.bDAO = bDAO;

            base.DisplayName = hero.name;

            UpdateCommand = new CommandViewModel(Resources.UpdateTalents,
                new RelayCommand(param => UpdateTalents()));
            CreateBuildCommand = new CommandViewModel(Resources.SaveBuild,
                new RelayCommand(param => CreateBuild()));
        }

        /// <summary>
        /// Populates list of abilities
        /// </summary>
        public void LoadAbilities()
        {
            List<AbilityViewModel> all = (from ability in hero.Ability
                                          select new AbilityViewModel(ability)).ToList();
            Abilities = new ObservableCollection<AbilityViewModel>(all);
        }

        /// <summary>
        /// Populates list of talents
        /// </summary>
        public void LoadTalents()
        {
            List<TalentViewModel> all = (from talent in hero.Talent
                                         select new TalentViewModel(talent)).ToList();
            Talents = new ObservableCollection<TalentViewModel>(all);
        }

        private void CreateBuild()
        {
            UpdateTalents();
            BuildViewModel build = new BuildViewModel(SelectedTalents, bDAO);
            DisplayWorkspaceMessage message = new DisplayWorkspaceMessage(build);
            Messenger.Default.Send<DisplayWorkspaceMessage>(message);
        }

        private void UpdateTalents()
        {
            var tmpSelectedTalents = SelectedTalents.Cast<TalentViewModel>().ToList();
            int?[] selectedTalentsID = new int?[tmpSelectedTalents.Count];
            for (int i = 0; i < SelectedTalents.Count; i++)
            {
                selectedTalentsID[i] = tmpSelectedTalents[i].ID;
            }
            
            foreach (var ability in Abilities)
            {
                ability.LoadEffects(selectedTalentsID);
            }
        } 

        public string Name
        {
            get
            {
                return hero.name;
            }
            set
            {
                if(value != hero.name)
                {
                    hero.name = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Title
        {
            get
            {
                return hero.title;
            }
            set
            {
                if(value != hero.title)
                {
                    hero.title = value;

                    OnPropertyChanged();
                }
            }
        }

        public int HP
        {
            get
            {
                return (int)Math.Round(hero.base_hp + Level * hero.hp_coefficient);
            }
        }

        public int Energy
        {
            get
            {
                return (int)Math.Round(hero.base_energy + Level * hero.energy_coefficient);
            }
        }

        public int AutoAttackDamage
        {
            get
            {
                return hero.base_attack_dmg + Level * hero.attack_dmg_coefficient;
            }
        }

        public float AutoAttackSpeed
        {
            get
            {
                return hero.attack_speed;
            }
            set
            {
                if(value != hero.attack_speed)
                {
                    hero.attack_speed = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Universe
        {
            get
            {
                return hero.Universum.name;
            }
        }

        public string Role
        {
            get
            {
                return hero.Role.title;
            }
        }

        public int Level { get; set; }
    }
}
