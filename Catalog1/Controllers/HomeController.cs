using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Catalog1.Controllers
{
    public class HomeController : Controller
    {
        [SiteMap]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Contents()
        {
            List<string> model = new List<string>();


            var allControllers = from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass && 
                    type.Namespace != null && 
                    type.Namespace.Contains("Controller")
                    select type;

            foreach(var controllerType in allControllers)
            {
                var allPublicMethodsOnController = controllerType
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance);

                foreach(var publicMethod in allPublicMethodsOnController)
                {
                    var sitemap = publicMethod.GetCustomAttributes(true)
                        .OfType<SiteMap>().FirstOrDefault();

                    if (sitemap != null)
                    {
                        model.Add(controllerType.Name.Replace("Controller", ""));
                    }
                }
            }

            return View(model);
        }
    }
}