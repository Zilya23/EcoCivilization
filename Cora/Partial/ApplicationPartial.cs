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
        public int PhotoWidth => 1400 / PhotoApplication.Count();
        public int PhotoHeight => 900 / PhotoApplication.Count();
    }
}
