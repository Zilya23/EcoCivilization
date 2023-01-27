using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cora.DataBase;

namespace Cora.Functionss
{
    public class ChatFunction
    {
        public static List<ChatUser> GetUserChatt(User user)
        {
            return bd_connection.connection.ChatUser.Where(x => x.IDUser == user.ID).ToList();
        }
    }
}
