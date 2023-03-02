using Microsoft.EntityFrameworkCore;

namespace EcoCivilizationAPI.Models
{
    public class DataAccess
    {
        public static List<Application> GetApplications()
        {
            return EcoCivilizationContext.GetContext().Applications
                   .Include(x => x.PhotoApplications)
                   .Include(x => x.ApplicationUsers)
                   .ToList();
        }

        public static List<User> GetUsers()
        {
            return EcoCivilizationContext.GetContext().Users
                   .Include(x => x.IdCityNavigation)
                   .Include(x => x.IdGenderNavigation)
                   .Include(x => x.IdRoleNavigation)
                   .ToList();
        }
    }
}
