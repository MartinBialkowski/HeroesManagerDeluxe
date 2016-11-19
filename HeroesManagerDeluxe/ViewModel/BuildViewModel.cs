using HeroesDomainModel;
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
        public ObservableCollection<TalentViewModel> SelectedTalents { get; set; }

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="build">Build Entity Object</param>
        public BuildViewModel(Build build)
        {
            this.build = build;
        }

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="talents">List of selected talents, List of TalentViewModel expected</param>
        public BuildViewModel(IList talents)
        {
            SelectedTalents = new ObservableCollection<TalentViewModel>(talents.Cast<TalentViewModel>().ToList());
        }

        /// <summary>
        /// Populates list of talents with TalentViewModels
        /// </summary>
        public void LoadTalents()
        {
            List<TalentViewModel> all = (from talent in build.Talent
                                         select new TalentViewModel(talent)).ToList();
            SelectedTalents = new ObservableCollection<TalentViewModel>(all);
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
                if(value != build.description)
                {
                    build.description = value;

                    OnPropertyChanged();
                }
            }
        }
    }
}
