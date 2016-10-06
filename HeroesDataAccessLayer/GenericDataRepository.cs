using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HeroesDataAccessLayer
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public virtual IList<T> GetAll(params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new HeroesDBEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }
                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new HeroesDBEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new HeroesDBEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }
                item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(where);
            }
            return item;
        }

        public void Add(params T[] items)
        {
            throw new NotImplementedException();
        }

        public void Update(params T[] items)
        {
            throw new NotImplementedException();
        }

        public void Remove(params T[] items)
        {
            throw new NotImplementedException();
        }
    }
}
