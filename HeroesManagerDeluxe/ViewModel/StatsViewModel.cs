using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe.ViewModel
{
    public class StatsViewModel : WorkspaceViewModel
    {
        private Stats stats;
        public AverageStatsViewModel AverageStats { get; set; }

        public StatsViewModel(Stats stats, Average_Stats averageStats)
        {
            this.stats = stats;
            this.AverageStats = new AverageStatsViewModel(averageStats);
        }

        public string User
        {
            get
            {
                return stats.User.name;
            }
        }

        public Hero HeroObject
        {
            get
            {
                return stats.Hero;
            }
        }

        public string Hero
        {
            get
            {
                return stats.Hero.name;
            }
        }

        public int GamesCount
        {
            get
            {
                return stats.number_of_games;
            }
            set
            {
                if(value != stats.number_of_games)
                {
                    stats.number_of_games = value;

                    OnPropertyChanged();
                }
            }
        }

        public float PlayerAverageHeroDamage
        {
            get
            {
                return stats.player_average_hero_damage;
            }
            set
            {
                if(value != stats.player_average_hero_damage)
                {
                    stats.player_average_hero_damage = value;

                    OnPropertyChanged();
                }
            }
        }

        public float PlayerAverageSiegeDamage
        {
            get
            {
                return stats.player_average_siege_damage;
            }
            set
            {
                if(value != stats.player_average_siege_damage)
                {
                    stats.player_average_siege_damage = value;

                    OnPropertyChanged();
                }
            }
        }

        public float PlayerAverageHealingDone
        {
            get
            {
                return stats.player_average_healing_done;
            }
            set
            {
                if(value != stats.player_average_healing_done)
                {
                    stats.player_average_healing_done = value;

                    OnPropertyChanged();
                }
            }
        }

        public float PlayerAverageDamageTaken
        {
            get
            {
                return stats.player_average_damage_taken;
            }
            set
            {
                if(value != stats.player_average_damage_taken)
                {
                    stats.player_average_damage_taken = value;

                    OnPropertyChanged();
                }
            }
        }

        public float PlayerAverageXP
        {
            get
            {
                return stats.player_average_xp_contribution;
            }
            set
            {
                if(value != stats.player_average_xp_contribution)
                {
                    stats.player_average_xp_contribution = value;

                    OnPropertyChanged();
                }
            }
        }

        public float PlayerAverageTakedowns
        {
            get
            {
                return stats.player_average_takedowns;
            }
            set
            {
                if(value != stats.player_average_takedowns)
                {
                    stats.player_average_takedowns = value;

                    OnPropertyChanged();
                }
            }
        }

        public float PlayerAverageKills
        {
            get
            {
                return stats.player_average_kills;
            }
            set
            {
                if(value != stats.player_average_kills)
                {
                    stats.player_average_kills = value;

                    OnPropertyChanged();
                }
            }
        }

        public float PlayerAverageDeaths
        {
            get
            {
                return stats.player_average_deaths;
            }
            set
            {
                if(value != stats.player_average_deaths)
                {
                    stats.player_average_deaths = value;

                    OnPropertyChanged();
                }
            }
        }

        public int HigestKillStreak
        {
            get
            {
                return stats.player_higest_kill_streak;
            }
            set
            {
                if(value != stats.player_higest_kill_streak)
                {
                    stats.player_higest_kill_streak = value;

                    OnPropertyChanged();
                }
            }
        }

        public int TimesAwarded
        {
            get
            {
                return stats.times_awarded;
            }
            set
            {
                if(value != stats.times_awarded)
                {
                    stats.times_awarded = value;

                    OnPropertyChanged();
                }
            }
        }

        public int TimesMVP
        {
            get
            {
                return stats.times_mvp;
            }
            set
            {
                if(value != stats.times_mvp)
                {
                    stats.times_mvp = value;

                    OnPropertyChanged();
                }
            }
        }


    }
}
