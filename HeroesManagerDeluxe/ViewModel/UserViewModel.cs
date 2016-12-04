using HeroesDomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesManagerDeluxe.ViewModel
{
    public class UserViewModel
    {
        private User user;

        public string Name
        {
            get
            {
                return user.name;
            }
        }
    }
}
