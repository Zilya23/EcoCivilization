using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.DataBase;

namespace Cora.Functionss
{
    public class AuthorizationFunction
    {
        public static List<User> users { get; set; }

        public static List<User> GetUsers()
        {
            return users = new List<User>(bd_connection.connection.User.ToList());
        }

        public static User AuthorizationUser(string email, string password)
        {
            users = new List<User>(bd_connection.connection.User.ToList());
            var userExsist = users.Where(user => user.Email == email && user.Password == password).FirstOrDefault();
            if(userExsist != null)
            {
                return userExsist;
            }
            else
            {
                return userExsist;
            }
        }
    }
}
