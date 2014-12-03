using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog1.Controllers
{
    [System.AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SiteMap : System.Attribute
    {
        public SiteMap()
        {

        }
    }
}