using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe.ViewModel
{
    public class AbilityEffectViewModel : WorkspaceViewModel
    {
        private Ability_Effect abilityEffect;
        public AbilityEffectViewModel(Ability_Effect abilityEffect)
        {
            this.abilityEffect = abilityEffect;

        }

        public string Name
        {
            get
            {
                return abilityEffect.Effect.name;
            }
        }

        public float Value
        {
            get
            {
                return abilityEffect.value;
            }
            set
            {
                if (value != abilityEffect.value)
                {
                    abilityEffect.value = value;

                    OnPropertyChanged();
                }
            }
        }

        public float Time
        {
            get
            {
                return abilityEffect.time;
            }
            set
            {
                if (value != abilityEffect.time)
                {
                    abilityEffect.time = value;

                    OnPropertyChanged();
                }
            }
        }

        public float TalentedValue
        {
            get
            {
                return abilityEffect.talented_value;
            }
            set
            {
                if (value != abilityEffect.talented_value)
                {
                    abilityEffect.talented_value = value;

                    OnPropertyChanged();
                }
            }
        }

        public float TalentedTime
        {
            get
            {
                return abilityEffect.talented_time;
            }
            set
            {
                if (value != abilityEffect.talented_time)
                {
                    abilityEffect.talented_time = value;

                    OnPropertyChanged();
                }
            }
        }
        public bool IsBaseEffect
        {
            get
            {
                if (abilityEffect.value != 0 || abilityEffect.time != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string ValueToDisplay { get; set; }
    }
}
