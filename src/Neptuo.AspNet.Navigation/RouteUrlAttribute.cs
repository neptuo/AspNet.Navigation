using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// Defines the route URL.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteUrlAttribute : Attribute, IRouteUrlProvider
    {
        /// <summary>
        /// Gets the route URL.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="url"/>.
        /// </summary>
        /// <param name="url">The route URL.</param>
        public RouteUrlAttribute(string url)
        {
            if (String.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            Url = url;
        }

        public string GetUrl()
        {
            return Uri;
        }
    }
}
