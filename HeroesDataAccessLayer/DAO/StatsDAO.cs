using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesDataAccessLayer
{
    public class StatsDAO : DAO, IGenericRepository<Stats>
    {
        /// <summary>
        /// Finds all stats from given user
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>IEnumerable of stats</returns>
        public IEnumerable<Stats> GetAllFromSpecifiedUser(int id)
        {
            return context.Stats.Where(s => s.user_id == id); 
        }

        /// <summary>
        /// Loads all stats from data base
        /// </summary>
        /// <returns>IEnumerable of stats</returns>
        public IEnumerable<Stats> GetAll()
        {
            return context.Stats;
        }

        /// <summary>
        /// Finds stats by specified id
        /// </summary>
        /// <param name="id">identificator of stats</param>
        /// <returns>First stats with specified id</returns>
        public Stats GetById(int id)
        {
            return context.Stats.FirstOrDefault(s => s.id == id);
        }

        /// <summary>
        /// Places the specified stats into data base
        /// </summary>
        public void Insert(Stats element)
        {
            context.Stats.Add(element);
        }

        /// <summary>
        /// Removes stats from date base by specified identificator
        /// </summary>
        public void Delete(int id)
        {
            context.Stats.Remove(GetById(id));
        }

        /// <summary>
        /// Updates given stats
        /// </summary>
        public void Update(Stats element)
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
