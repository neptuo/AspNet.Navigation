using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// Defines the reflected values from route model.
    /// </summary>
    public interface IRouteModel
    {
        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the URL of the route.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Gets the defaults values of the route.
        /// </summary>
        IDictionary<string, object> Defaults { get; }

        /// <summary>
        /// Gets constraints for the route parameters.
        /// </summary>
        IDictionary<string, object> Constraints { get; }
    }
}
