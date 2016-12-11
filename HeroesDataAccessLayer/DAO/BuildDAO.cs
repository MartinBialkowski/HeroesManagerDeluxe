using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesDataAccessLayer
{
    public class BuildDAO : DAO, IGenericRepository<Build>
    {
        /// <summary>
        /// Loads all builds from data base
        /// </summary>
        /// <returns>IEnumerable of builds</returns>
        public IEnumerable<Build> GetAll()
        {
            return context.Build;
        }

        /// <summary>
        /// Finds build by specified id
        /// </summary>
        /// <param name="id">identificator of build</param>
        /// <returns>First build with specified id</returns>
        public Build GetById(int id)
        {
            return context.Build.FirstOrDefault(b => b.id == id); 
        }

        /// <summary>
        /// Places the specified build into data base
        /// </summary>
        public void Insert(Build element)
        {
            context.Build.Add(element);
        }

        /// <summary>
        /// Removes build from date base by specified identificator
        /// </summary>
        public void Delete(int id)
        {
            context.Build.Remove(GetById(id));
        }

        /// <summary>
        /// Updates given build
        /// </summary>
        public void Update(Build element)
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
