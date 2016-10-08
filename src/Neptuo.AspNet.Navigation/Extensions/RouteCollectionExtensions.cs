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
    /// The extensions for registering model routes.
    /// </summary>
    public static class RouteCollectionExtensions
    {
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
