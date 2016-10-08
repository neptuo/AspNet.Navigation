using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc
{
    /// <summary>
    /// The extensions for generating URLs for model routes.
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Generates a fully qualified URL for the route defined by the <paramref name="route"/> mode;.
        /// </summary>
        /// <param name="url">The helper for generating URLs.</param>
        /// <param name="route">The instance of the model route.</param>
        /// <returns>The fully qualified URL.</returns>
        public static string ModelUrl(this UrlHelper url, object route)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return url.RouteUrl(model.Name, route);
        }
    }
}