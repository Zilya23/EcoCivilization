﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.DataBase;

namespace Cora.Functionss
{
    public class StatisticFunction
    {
        //количество заявок созданных пользователем
        public static int CountApplication(User user)
        {
            return bd_connection.connection.Application.Where(x => x.IDUser == user.ID).Count();
        }

        //количество заявок в которых пользователь участвовал
        public static int CountFollowApplication(User user)
        {
            return bd_connection.connection.Application_User.Where(x => x.ID_User == user.ID).Count();
        }

        //количество мероприятий в этом городе
        public static int CountApplicationCity(City city)
        {
            return bd_connection.connection.Application.Where(x => x.ID_City == city.ID).Count();
        }

        //Возвращает все заявки созданные в этом году
        public static int ApplicationYear()
        {
            return bd_connection.connection.Application.Where(x => Convert.ToDateTime(x.Date).Year == DateTime.Now.Year).Count();
        }

        //количество пользователей из города пользователя
        public static int CountUserInCity(User user)
        {
            return bd_connection.connection.User.Where(x => x.ID_City == user.ID_City).Count();
        }
    }
}