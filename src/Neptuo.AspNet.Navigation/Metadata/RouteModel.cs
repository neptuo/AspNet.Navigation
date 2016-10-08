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
        private static IRouteModelCollection collection = new DefaultRouteModelCollection();

        /// <summary>
        /// Gets the singleton of the <see cref="IRouteModelCollection"/>.
        /// </summary>
        public static IRouteModelCollection Collection
        {
            get { return collection; }
        }

        /// <summary>
        /// Sets the singleton of the <see cref="IRouteModelCollection"/>.
        /// </summary>
        /// <param name="collection">The instalce of collection to be used.</param>
        /// <exception cref="ArgumentNullException">When the <paramref name="collection"/> is <c>null</c>.</exception>
        public static void SetCollection(IRouteModelCollection collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            RouteModel.collection = collection;
        }
    }
}
