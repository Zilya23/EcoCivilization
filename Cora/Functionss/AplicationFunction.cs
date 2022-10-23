using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.DataBase;

namespace Cora.Functionss
{
    public class AplicationFunction
    {
        public static List<Application> GetAllApplications()
        {
            List<Application> applications = bd_connection.connection.Application.ToList();
            return applications;
        }
    }
}
