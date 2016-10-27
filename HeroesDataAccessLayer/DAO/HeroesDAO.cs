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

        public IEnumerable<Hero> GetAll()
        {
            return context.Hero;
        }

        public Hero GetById(int id)
        {
            return context.Hero.FirstOrDefault(c => c.id == id);
        }

        public void Insert(Hero element)
        {
            context.Hero.Add(element);
        }

        public void Delete(int id)
        {
            Hero hero = context.Hero.Find(id);
            context.Hero.Remove(hero);
        }

        public void Update(Hero element)
        {
            context.Entry(element).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
