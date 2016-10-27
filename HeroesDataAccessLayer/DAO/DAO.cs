using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesDataAccessLayer
{
    public class DAO
    {
        protected HeroesDBEntities context;

        public DAO()
        {
            context = DBConnection.Context;
        }
    }
}
