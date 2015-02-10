using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITINDEXAU.Class
{
    public class Connection : Controller
    {
        public string connection()
        {
            return "Data Source=USER-PC\\SQLEXPRESS;database=ITINDEX_DB;integrated security=SSPI";           
        }       

    }
}
