using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Cora.Functionss;
using Cora.DataBase;

namespace EcoCivilizationAPI.Services
{
    public static class UserServices
    {
        public static List<User> users = AuthorizationFunction.GetUsers();

        public static User AuthorizationUser(string login, string password)
        {
            var userExsist = users.Where(user => user.Login == login && user.Password == password).FirstOrDefault();
            return userExsist;
        }
    }
}
