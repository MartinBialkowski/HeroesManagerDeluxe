using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe.ViewModel
{
    public class AbilityViewModel : ViewModelBase
    {
        private Ability ability;
        public ObservableCollection<AbilityEffectViewModel> Effects;
        public int Level { get; set; }
        public AbilityViewModel(Ability ability)
        {
            this.ability = ability;
            LoadEffects();
        }

        public void LoadEffects()
        {
            List<AbilityEffectViewModel> all = (from effect in ability.Ability_Effect
                                              select new AbilityEffectViewModel(effect)).ToList();
            Effects = new ObservableCollection<AbilityEffectViewModel>(all);
        }

        public string Name
        {
            get
            {
                return ability.name;
            }
            set
            {
                if(value != ability.name)
                {
                    ability.name = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return ability.description;
            }
            set
            {
                if(value != ability.description)
                {
                    ability.description = value;

                    OnPropertyChanged();
                }
            }
        }

        public float XPCoefficient
        {
            get
            {
                return ability.xp_coefficient;
            }
            set
            {
                if(value != ability.xp_coefficient)
                {
                    ability.xp_coefficient = value;

                    OnPropertyChanged(); //ErrPos if OnPropertyChanged not works, mb name must be same as name in model
                }
            }
        }

        
    }
}
