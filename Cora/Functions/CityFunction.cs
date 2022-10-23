using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.DataBase;

namespace Cora
{
    public class CityFunction
    {
        public static List<City> GetCities()
        {
            List<City> cities = bd_connection.connection.City.ToList();
            return cities;
        }

        public static List<Gender> GetGenders()
        {
            List<Gender> genders = bd_connection.connection.Gender.ToList();
            return genders;
        }
    }
}
