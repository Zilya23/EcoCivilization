using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.DataBase;
using Cora.Functionss;

namespace Cora.Functionss
{
    public class RegistFunction
    {
        public static bool Registration(User userR)
        {
            User user = new User();

            if (UniqUser(userR.Email, userR.Telephone))
            {
                user.Name = userR.Name;
                user.Surname = userR.Surname;
                user.Telephone = userR.Telephone;
                user.ID_City = userR.ID_City;
                user.ID_Gender = userR.ID_Gender;
                user.Email = userR.Email;
                user.Password = userR.Password;
                user.ID_Role = userR.ID_Role;
                user.Count_Application = userR.Count_Application;
                user.DateRegist = DateTime.Now;
                bd_connection.connection.User.Add(user);
                bd_connection.connection.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool UniqUser(string email, string telephone)
        {
            List<User> users = bd_connection.connection.User.ToList();
            User uniqUser = users.Where(x => x.Email == email || x.Telephone == telephone).FirstOrDefault();
            if (uniqUser == null)
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
