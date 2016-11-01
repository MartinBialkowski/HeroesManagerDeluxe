using HeroesDataAccessLayer;
using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe.ViewModel
{
    public class HeroDetailsViewModel : WorkspaceViewModel
    {
        private Hero hero;
        private HeroesDAO hDAO;
        public ObservableCollection<AbilityViewModel> Abilities { get; private set; }
        public ObservableCollection<TalentViewModel> Talents { get; private set; }

        public HeroDetailsViewModel(Hero hero, HeroesDAO hDAO)
        {
            this.hero = hero;
            this.hDAO = hDAO;

            base.DisplayName = hero.name;
        }

        public void LoadAbilities()
        {
            List<AbilityViewModel> all = (from ability in hero.Ability
                                          select new AbilityViewModel(ability)).ToList();
            Abilities = new ObservableCollection<AbilityViewModel>(all);   
        }

        public void LoadTalents()
        {
            List<TalentViewModel> all = (from talent in hero.Talent
                                         select new TalentViewModel(talent)).ToList();
            Talents = new ObservableCollection<TalentViewModel>(all);
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
