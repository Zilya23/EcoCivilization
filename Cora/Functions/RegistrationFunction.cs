using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.DataBase;

namespace Cora
{
    public class RegistrationFunction
    {
        public static bool Registration(User userR)
        {
            User user = new User();

            if(UniqUser(userR.Login, userR.Telephone))
            {
                user.Name = userR.Name;
                user.Surname = userR.Surname;
                user.Telephone = userR.Telephone;
                user.ID_City = userR.ID_City;
                user.ID_Gender = userR.ID_Gender;
                user.Login = userR.Login;
                user.Password = userR.Password;
                user.ID_Role = userR.ID_Role;
                user.Count_Application = userR.Count_Application;
                bd_connection.connection.User.Add(user);
                bd_connection.connection.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool UniqUser(string login, string telephone)
        {
            List<User> users = bd_connection.connection.User.ToList();
            User uniqUser = users.Where(x => x.Login == login || x.Telephone == telephone).FirstOrDefault();
            if(uniqUser == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
