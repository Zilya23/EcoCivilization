using Cora.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cora.Models
{
    public class ApplicationModel
    {
        public ApplicationModel(Application application)
        {
            ID = application.ID;
            Name= application.Name;
            Date = application.Date;
            Count_User = application.Count_User;
            Place = application.Place;
            Description = application.Description;
            ID_City = application.ID_City;
            IDUser = application.IDUser;
            TimeStart = application.TimeStart;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Count_User { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public Nullable<int> ID_City { get; set; }
        public Nullable<int> IDUser { get; set; }
        public Nullable<System.TimeSpan> TimeStart { get; set; }
    }

    
}
