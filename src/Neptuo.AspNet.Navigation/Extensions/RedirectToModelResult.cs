using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace System.Web.Mvc
{
    /// <summary>
    /// Represents a result that performs a redirection by using the model route.
    /// </summary>
    public class RedirectToModelResult : RedirectToRouteResult
    {
        /// <summary>
        /// Creates new instance that redirects to the model <paramref name="route"/>.
        /// </summary>
        /// <param name="route">The instance of model route,</param>
        public RedirectToModelResult(object route)
            : base(GetRouteName(route), new RouteValueDictionary(route))
        { }

        /// <summary>
        /// Creates new instance that redirects to the model <paramref name="route"/> and permanent-redirection flag.
        /// </summary>
        /// <param name="route">The instance of model route,</param>
        /// <param name="permanent">A value that indicates whether the redirection should be permanent.</param>
        public RedirectToModelResult(object route, bool permanent)
            : base(GetRouteName(route), new RouteValueDictionary(route), permanent)
        { }

        /// <summary>
        /// Gets the name of the route from the <paramref name="route"/>.
        /// </summary>
        /// <param name="route">The instance of model route,</param>
        /// <returns>The name of the route.</returns>
        private static string GetRouteName(object route)
        {
            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return model.Name;
        }
    }
}
