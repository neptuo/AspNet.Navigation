using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// Defines the provider of route constraints.
    /// </summary>
    public interface IRouteConstraintProvider
    {
        /// <summary>
        /// Returns the enumeration of key-value pairs for the route parameters constraints.
        /// </summary>
        /// <returns>The enumeration of key-value pairs for the route parameters constraints.</returns>
        IEnumerable<KeyValuePair<string, string>> GetUrlConstraints();
    }
}
