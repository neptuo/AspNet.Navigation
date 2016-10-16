using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// Un-named route with custom defaults.
    /// </summary>
    [RouteUrl("{controller}/{action}/{id}")]
    [RouteController("Home", "Index")]
    [RouteDefault("id", null)]
    [RouteConstraintRegex("id", @"\d*")]
    public class MvcRoute
    {
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public int? Id { get; private set; }

        public MvcRoute(string controller, string action)
        {
            Controller = controller;
            Action = action;
        }

        public MvcRoute(string controller, string action, int id)
        {
            Controller = controller;
            Action = action;
            Id = id;
        }
    }
}
