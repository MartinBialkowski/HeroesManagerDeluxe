using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesDataAccessLayer
{
    public class HeroesDAO : DAO, IGenericRepository<Hero>
    {

        /// <summary>
        /// Loads all heros from data base
        /// </summary>
        /// <returns>IEnumerable of heros</returns>
        public IEnumerable<Hero> GetAll()
        {
            return context.Hero;
        }

        /// <summary>
        /// Finds hero by specified id.
        /// </summary>
        /// <param name="id">identificator of hero</param>
        /// <returns>First hero with specified id</returns>
        public Hero GetById(int id)
        {
            return context.Hero.FirstOrDefault(c => c.id == id);
        }

        /// <summary>
        /// Places the specified hero into data base
        /// </summary>
        public void Insert(Hero element)
        {
            context.Hero.Add(element);
        }

        /// <summary>
        /// Removes hero from date base by specified identificator
        /// </summary>
        public void Delete(int id)
        {           
            context.Hero.Remove(GetById(id));
        }

        /// <summary>
        /// Updates given hero
        /// </summary>
        public void Update(Hero element)
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
