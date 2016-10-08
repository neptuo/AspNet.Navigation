using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// Defines the provider of route name.
    /// </summary>
    public interface IRouteNameProvider
    {
        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        /// <returns>The name of the route.</returns>
        string GetName();
    }
}
