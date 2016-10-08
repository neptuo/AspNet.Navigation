using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System.Web.Mvc
{
    /// <summary>
    /// The extensions for model routing.
    /// </summary>
    public static class MvcExtensions
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

        /// <summary>
        /// Returns an anchor element (a element) that contains the path defined by the <paramref name="route"/> model.
        /// </summary>
        /// <param name="html">The helper for generating HTML.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="route">The instance of the model route.</param>
        /// <returns>An anchor element (a element).</returns>
        public static MvcHtmlString ModelLink(this HtmlHelper html, string linkText, object route)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return html.RouteLink(linkText, model.Name, route);
        }

        /// <summary>
        /// Maps the specified URL route defined by the <typeparamref name="TRoute"/> model.
        /// </summary>
        /// <typeparam name="TRoute">The type of the model route.</typeparam>
        /// <param name="routes">The collection of the routes.</param>
        /// <returns>A reference to the mapped route.</returns>
        public static Route MapModel<TRoute>(this RouteCollection routes)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");

            IRouteModel model = RouteModel.Provider.Get(typeof(TRoute));
            return routes.MapRoute(
                name: model.Name, 
                url: model.Url, 
                defaults: model.Defaults
            );
        }
    }
}