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

        public static User AuthorizationUser(string login, string password)
        {
            users = new List<User>(bd_connection.connection.User.ToList());
            var userExsist = users.Where(user => user.Login == login && user.Password == password).FirstOrDefault();
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
