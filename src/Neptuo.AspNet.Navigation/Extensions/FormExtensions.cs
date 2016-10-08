using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// The extensions for generating forms for model routes.
    /// </summary>
    public static class FormExtensions
    {
        /// <summary>
        /// Writes an opening &lt;form&gt; tag to the response. When the user submits the form, the request will be processed by the route target.
        /// </summary>
        /// <param name="html">The helper for generating HTML.</param>
        /// <param name="route">The instance of the model route.</param>
        /// <returns>An opening &lt;form&gt; tag.</returns>
        public static MvcForm BeginModelForm(this HtmlHelper html, object route)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return html.BeginRouteForm(model.Name, route);
        }

        /// <summary>
        /// Writes an opening &lt;form&gt; tag to the response. When the user submits the form, the request will be processed by the route target.
        /// </summary>
        /// <param name="html">The helper for generating HTML.</param>
        /// <param name="route">The instance of the model route.</param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>An opening &lt;form&gt; tag.</returns>
        public static MvcForm BeginModelForm(this HtmlHelper html, object route, FormMethod method)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return html.BeginRouteForm(model.Name, route, method);
        }

        /// <summary>
        /// Writes an opening &lt;form&gt; tag to the response. When the user submits the form, the request will be processed by the route target.
        /// </summary>
        /// <param name="html">The helper for generating HTML.</param>
        /// <param name="route">The instance of the model route.</param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An opening &lt;form&gt; tag.</returns>
        public static MvcForm BeginModelForm(this HtmlHelper html, object route, FormMethod method, object htmlAttributes)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return html.BeginRouteForm(model.Name, route, method, htmlAttributes);
        }

        /// <summary>
        /// Writes an opening &lt;form&gt; tag to the response. When the user submits the form, the request will be processed by the route target.
        /// </summary>
        /// <param name="html">The helper for generating HTML.</param>
        /// <param name="route">The instance of the model route.</param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An opening &lt;form&gt; tag.</returns>
        public static MvcForm BeginModelForm(this HtmlHelper html, object route, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            if (html == null)
                throw new ArgumentNullException("html");

            if (route == null)
                throw new ArgumentNullException("route");

            IRouteModel model = RouteModel.Provider.Get(route.GetType());
            return html.BeginRouteForm(model.Name, route, method, htmlAttributes);
        }
    }
}
