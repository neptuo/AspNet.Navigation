using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// Defines the provider of the route URL.
    /// </summary>
    public interface IRouteUrlProvider
    {
        /// <summary>
        /// Gets the URL of the route.
        /// </summary>
        /// <returns>The URL of the route.</returns>
        string GetUrl();
    }
}
