using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSQL
{
    public class MySQL : AbstractSQL
    {
        public override IMethod CreateSQL()
        {
            return new MySQLMethod();
        }
    }
}
