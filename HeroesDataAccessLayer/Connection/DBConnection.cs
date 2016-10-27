using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesDataAccessLayer
{
    public static class DBConnection
    {
        public static HeroesDBEntities Context { get; set; }
        static DBConnection()
        {
            Context = new HeroesDBEntities();
        }

        public static void Reconnect()
        {
            Context = new HeroesDBEntities();
        }
    }
}