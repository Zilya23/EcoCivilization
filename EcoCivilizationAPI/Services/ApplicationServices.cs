using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Cora.Functionss;
using Cora.DataBase;

namespace EcoCivilizationAPI.Services
{
    public static class ApplicationServices
    {
        
        
        static List<Application> applications = AplicationFunction.GetAllApplications();
        public static List<Application> GetAllAplications() => applications;
        public static Application GetApplication(int ID) => applications.FirstOrDefault(a => a.ID == ID);

        public static List<Application> GetFilterApplication(DateTime selectedDate, int IDCity)
        {
            return applications.Where(x=> x.ID_City == IDCity && x.Date == selectedDate).ToList();
        }
    }
}
