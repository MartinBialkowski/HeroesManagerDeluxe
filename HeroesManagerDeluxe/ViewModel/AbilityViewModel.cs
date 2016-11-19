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
        public ObservableCollection<AbilityEffectViewModel> AvailableEffects { get; set; }
        public int Level { get; set; }

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="ability">Entity object</param>
        public AbilityViewModel(Ability ability)
        {
            this.ability = ability;
            LoadEffects();
        }

        /// <summary>
        /// Creates AbilityEffectViewModel objects and initialize AvailableEffects list
        /// </summary>
        public void LoadEffects()
        {
            List<AbilityEffectViewModel> all = (from effect in ability.Ability_Effect
                                                .Where(a => a.value != 0 || a.time != 0)
                                                select new AbilityEffectViewModel(effect)).ToList();

            AvailableEffects = new ObservableCollection<AbilityEffectViewModel>(all);
            foreach (var item in AvailableEffects)
            {
                BuildValueToDisplay(false, item);
            }
        }

        /// <summary>
        /// Updates effects and creates new if not exist using list of selected talents ID
        /// </summary>
        /// <param name="talentsID">List of selected talents ID,
        /// list contains only not null values</param>
        public void LoadEffects(int?[] talentsID)
        {
            ResetTalents();
            var intersectedIDs = AvailableEffects.Where(a => a.TalentId != null)
                .Select(a => a.TalentId).ToList()
                .Intersect(talentsID).ToList();
            //Seek in active effects and modify them
            foreach (var Id in intersectedIDs)
            {
                foreach (var item in AvailableEffects.Where(a => a.TalentId == Id))
                {
                    BuildValueToDisplay(true, item);
                }
            }

            intersectedIDs = ability.Ability_Effect.Where(a => a.talent_id != null && 
                a.value == 0 || a.time == 0)
                .Select(a => a.talent_id)
                .ToList().Intersect(talentsID).ToList();
            //Seek in all effects and creates new ones
            foreach (var Id in intersectedIDs)
            {
                List<AbilityEffectViewModel> newEffects = (from effect in ability.Ability_Effect
                                                    .Where(a => a.talent_id == Id &&
                                                    a.value == 0 || a.time == 0)
                                                    select new AbilityEffectViewModel(effect)).ToList();
                foreach (var item in newEffects)
                {
                    BuildValueToDisplay(true, item);
                    AvailableEffects.Add(item);
                }
            }
        }

        /// <summary>
        /// Removes effects from talents, and sets untalented values
        /// </summary>
        private void ResetTalents()
        {
            var effectsToDelete = AvailableEffects.Where(a => !a.IsBaseEffect).ToList();
            foreach (var effect in effectsToDelete)
            {
                AvailableEffects.Remove(effect);
            }

            foreach (var effect in AvailableEffects)
            {
                BuildValueToDisplay(false, effect);
            }
        }

        /// <summary>
        /// Sets value which will be seen by user
        /// </summary>
        /// <param name="isTalented">Determine if values should be set to talented or untalented values</param>
        /// <param name="effect">ViewModel currently edited</param>
        private void BuildValueToDisplay(bool isTalented, AbilityEffectViewModel effect)
        {
            effect.ValueToDisplay = string.Empty;
            if(isTalented)
            {
                if (effect.TalentedValue != 0)
                {
                    effect.ValueToDisplay = effect.TalentedValue.ToString() + " " + effect.Helper;
                }
                if (effect.TalentedTime != 0)
                {
                    effect.ValueToDisplay += " " + effect.TalentedTime.ToString() + " sec";
                }
            }
            else
            {
                if (effect.Value != 0)
                {
                    effect.ValueToDisplay = effect.Value.ToString() + " " + effect.Helper;
                }
                if (effect.Time != 0)
                {
                    effect.ValueToDisplay += " " + effect.Time.ToString() + " sec";
                }
            }
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
