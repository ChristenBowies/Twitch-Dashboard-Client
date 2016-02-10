using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardClient
{
    class XRF
    {
        public string generateState()
        {
            return Guid.NewGuid().ToString().Substring(0, 150);
        }
    }
}
