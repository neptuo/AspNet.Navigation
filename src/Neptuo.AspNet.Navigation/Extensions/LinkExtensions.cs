using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// The extensions for generating model route links.
    /// </summary>
    public static class LinkExtensions
    {
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
        /// Returns an anchor element (a element) that contains the path defined by the <paramref name="route"/> model.
        /// </summary>
        /// <param name="html">The helper for generating HTML.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="route">The instance of the model route.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An anchor element (a element).</returns>
        public static MvcHtmlString ModelLink(this HtmlHelper html, string linkText, object route, object htmlAttributes)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return html.RouteLink(linkText, model.Name, route, htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor element (a element) that contains the path defined by the <paramref name="route"/> model.
        /// </summary>
        /// <param name="html">The helper for generating HTML.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="route">The instance of the model route.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An anchor element (a element).</returns>
        public static MvcHtmlString ModelLink(this HtmlHelper html, string linkText, object route, IDictionary<string, object> htmlAttributes)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return html.RouteLink(linkText, model.Name, route, htmlAttributes);
        }

    }
}
