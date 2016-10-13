using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// The singleton class used in extension methods for registering routes, creating URLs and links.
    /// </summary>
    public static class RouteModel
    {
        private static IRouteModelProvider provider = new CacheRouteModelProvider(new ReflectionRouteModelProvider());

        /// <summary>
        /// Gets the singleton of the <see cref="IRouteModelProvider"/>.
        /// </summary>
        public static IRouteModelProvider Provider
        {
            get { return provider; }
        }

        /// <summary>
        /// Sets the singleton of the <see cref="IRouteModelProvider"/>.
        /// </summary>
        /// <param name="provider">The instalce of collection to be used.</param>
        /// <exception cref="ArgumentNullException">When the <paramref name="provider"/> is <c>null</c>.</exception>
        public static void SetProvider(IRouteModelProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("collection");

            RouteModel.provider = provider;
        }
    }
}
