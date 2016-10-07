using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// Defines the provider of route default values.
    /// </summary>
    public interface IRouteDefaultProvider
    {
        /// <summary>
        /// Returns the enumeration of key-value pairs for the route defauls.
        /// </summary>
        /// <returns>The enumeration of key-value pairs for the route defauls.</returns>
        IEnumerable<KeyValuePair<string, object>> GetKeyValues();
    }
}
