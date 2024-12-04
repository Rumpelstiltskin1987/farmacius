using System.Configuration;
using System.Data.SQLite;
using farmacius.Models;
using farmacius.DataBase;

namespace farmacius.Logic
{

    public class UserLogic : IUserLogic
    {
        public UserLogic() {

        }


        public User GetUser(Login user)
        {

            var userAux = new User();

            return userAux;
        }
    }
}
