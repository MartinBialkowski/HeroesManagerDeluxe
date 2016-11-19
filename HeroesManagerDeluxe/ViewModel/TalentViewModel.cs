using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesDomainModel;

namespace HeroesManagerDeluxe.ViewModel
{
    public class TalentViewModel : ViewModelBase
    {
        private Talent talent;

        public TalentViewModel(Talent talent)
        {
            this.talent = talent;
        }

        public int ID
        {
            get
            {
                return talent.id;
            }
        }

        public string Name
        {
            get
            {
                return talent.name;
            }
            set
            {
                if(value != talent.name)
                {
                    talent.name = value;

                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return talent.description;
            }
            set
            {
                talent.description = value;

                OnPropertyChanged();
            }
        }

        public int Level
        {
            get
            {
                return talent.lvl;
            }
            set
            {
                if(value != talent.lvl)
                {
                    talent.lvl = value;

                    OnPropertyChanged();
                }
            }
        }

        public Talent ReturnTalent()
        {
            return talent;
        }
    }
}
