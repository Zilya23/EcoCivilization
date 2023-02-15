using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.DataBase;

namespace Cora.DataBase
{
    public partial class Application
    {
        public Application(Application application)
        {
            ID = application.ID;
            Name = application.Name;
            Date = application.Date;
            Count_User = application.Count_User;
            Place = application.Place;
            Description = application.Description;
            ID_City = application.ID_City;
            IDUser = application.IDUser;
            TimeStart = application.TimeStart;
            PhotoApplication = new List<PhotoApplication>
            {
                new PhotoApplication()
            };
        }

        public int PhotoWidth => 1200 / PhotoApplication.Count();
        public int PhotoHeight => 900 / PhotoApplication.Count();

        public int PhotoWidthAddPage => (int)(400 / Math.Sqrt(PhotoApplication.Count()));
        public int PhotoHeihtAddPage => (int)(300 / Math.Sqrt(PhotoApplication.Count()));

    }
}
