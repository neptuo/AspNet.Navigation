using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// Defines the provider of cached values created from the route models.
    /// </summary>
    public interface IRouteModelProvider
    {
        /// <summary>
        /// Gets the metadata for the <paramref name="modelType"/>.
        /// </summary>
        /// <param name="modelType">The type of route model.</param>
        /// <returns>The metadata for the <paramref name="modelType"/>.</returns>
        IRouteModel Get(Type modelType);
    }
}
