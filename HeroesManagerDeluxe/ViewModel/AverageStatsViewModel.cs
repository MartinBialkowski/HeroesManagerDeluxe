using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe.ViewModel
{
    public class AverageStatsViewModel : ViewModelBase
    {
        private Average_Stats averageStats;

        public AverageStatsViewModel(Average_Stats averageStats)
        {
            this.averageStats = averageStats;
        }

        public int GamesCount
        {
            get
            {
                return averageStats.number_of_games;
            }
            set
            {
                if (value != averageStats.number_of_games)
                {
                    averageStats.number_of_games = value;

                    OnPropertyChanged();
                }
            }
        }

        public float AverageHeroDamage
        {
            get
            {
                return averageStats.average_hero_damage;
            }
            set
            {
                if (value != averageStats.average_hero_damage)
                {
                    averageStats.average_hero_damage = value;

                    OnPropertyChanged();
                }
            }
        }

        public float AverageSiegeDamage
        {
            get
            {
                return averageStats.average_siege_damage;
            }
            set
            {
                if (value != averageStats.average_siege_damage)
                {
                    averageStats.average_siege_damage = value;

                    OnPropertyChanged();
                }
            }
        }

        public float AverageHealingDone
        {
            get
            {
                return averageStats.average_healing_done;
            }
            set
            {
                if (value != averageStats.average_healing_done)
                {
                    averageStats.average_healing_done = value;

                    OnPropertyChanged();
                }
            }
        }

        public float AverageDamageTaken
        {
            get
            {
                return averageStats.average_damage_taken;
            }
            set
            {
                if (value != averageStats.average_damage_taken)
                {
                    averageStats.average_damage_taken = value;

                    OnPropertyChanged();
                }
            }
        }

        public float AverageXP
        {
            get
            {
                return averageStats.average_xp_contribution;
            }
            set
            {
                if (value != averageStats.average_xp_contribution)
                {
                    averageStats.average_xp_contribution = value;

                    OnPropertyChanged();
                }
            }
        }

        public float AverageTakedowns
        {
            get
            {
                return averageStats.average_takedowns;
            }
            set
            {
                if (value != averageStats.average_takedowns)
                {
                    averageStats.average_takedowns = value;

                    OnPropertyChanged();
                }
            }
        }

        public float AverageKills
        {
            get
            {
                return averageStats.average_kills;
            }
            set
            {
                if (value != averageStats.average_kills)
                {
                    averageStats.average_kills = value;

                    OnPropertyChanged();
                }
            }
        }

        public float AverageDeaths
        {
            get
            {
                return averageStats.average_deaths;
            }
            set
            {
                if (value != averageStats.average_deaths)
                {
                    averageStats.average_deaths = value;

                    OnPropertyChanged();
                }
            }
        }

        public int HigestKillStreak
        {
            get
            {
                return averageStats.higest_kill_streak;
            }
            set
            {
                if (value != averageStats.higest_kill_streak)
                {
                    averageStats.higest_kill_streak = value;

                    OnPropertyChanged();
                }
            }
        }

        public int TimesAwarded
        {
            get
            {
                return averageStats.times_awarded;
            }
            set
            {
                if (value != averageStats.times_awarded)
                {
                    averageStats.times_awarded = value;

                    OnPropertyChanged();
                }
            }
        }

        public int TimesMVP
        {
            get
            {
                return averageStats.times_mvp;
            }
            set
            {
                if (value != averageStats.times_mvp)
                {
                    averageStats.times_mvp = value;

                    OnPropertyChanged();
                }
            }
        }
    }
}
