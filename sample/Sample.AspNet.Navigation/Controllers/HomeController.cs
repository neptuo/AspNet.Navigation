using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neptuo.AspNet.Navigation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome home.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About page.";
            return View("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";
            return View("Index");
        }

        public ActionResult Blog(int year, int month, int day, string url)
        {
            DateTime publishedAt = new DateTime(year, month, day);
            ViewBag.Message = String.Format("Blog post from '{0:yyyy-MM-dd}' with url '{1}'", publishedAt, url);
            return View("Index");
        }

        public ActionResult Project(string name)
        {
            ViewBag.Message = String.Format("Project named '{0}'.", name);
            return View("Index");
        }
    }
}