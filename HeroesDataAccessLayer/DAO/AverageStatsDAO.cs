using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesDataAccessLayer
{
    public class AverageStatsDAO : DAO, IGenericRepository<Average_Stats>
    {

        /// <summary>
        /// Gets stats for given hero id
        /// </summary>
        /// <param name="id">identificator of hero</param>
        /// <returns>First stats for specified hero</returns>
        public Average_Stats GetStatsForSpecifiedHero(int id)
        {
            return context.Average_Stats.FirstOrDefault(a => a.hero_id == id);
        }
        /// <summary>
        /// Loads all stats from data base
        /// </summary>
        /// <returns>IEnumerable of stats</returns>
        public IEnumerable<Average_Stats> GetAll()
        {
            return context.Average_Stats;
        }

        /// <summary>
        /// Finds stats by specified id.
        /// </summary>
        /// <param name="id">identificator of stats</param>
        /// <returns>First stats with specified id</returns>
        public Average_Stats GetById(int id)
        {
            return context.Average_Stats.FirstOrDefault(a => a.id == id);
        }

        /// <summary>
        /// Places the specified stats into data base
        /// </summary>
        public void Insert(Average_Stats element)
        {
            context.Average_Stats.Add(element);
        }

        /// <summary>
        /// Removes stats from date base by specified identificator
        /// </summary>
        public void Delete(int id)
        {
            context.Average_Stats.Remove(GetById(id));
        }

        /// <summary>
        /// Updates given stats
        /// </summary>
        public void Update(Average_Stats element)
        {
            context.Entry(element).State = EntityState.Modified;
        }

        /// <summary>
        /// Saves context
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
