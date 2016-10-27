using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe.ViewModel
{
    public class EffectViewModel : WorkspaceViewModel
    {
        private Effect effect;
        
        public EffectViewModel(Effect effect)
        {
            this.effect = effect;
        }

        public string Name
        {
            get
            {
                return effect.name;
            }
            set
            {
                if (value != effect.name)
                {
                    effect.name = value;

                    OnPropertyChanged();
                }
            }
        }
    }
}
