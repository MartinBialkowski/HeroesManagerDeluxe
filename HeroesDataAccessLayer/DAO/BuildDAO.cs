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
        public IEnumerable<Build> GetAll()
        {
            return context.Build;
        }

        public Build GetById(int id)
        {
            Build build = context.Build.FirstOrDefault(b => b.id == id);
            return build;
        }

        public void Insert(Build element)
        {
            context.Build.Add(element);
        }

        public void Delete(int id)
        {
            Build build = context.Build.FirstOrDefault(b => b.id == id);
            context.Build.Remove(build);
        }

        public void Update(Build element)
        {
            context.Entry(element).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
