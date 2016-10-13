using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// Defines a name of the route.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteNameAttribute : Attribute, IRouteNameProvider
    {
        /// <summary>
        /// Gets the name of the route.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates new instance with implicit name taken by used convention.
        /// </summary>
        public RouteNameAttribute()
        { }

        /// <summary>
        /// Creates new instance with explicit <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the route.</param>
        public RouteNameAttribute(string name)
        {
            Name = name;
        }

        string IRouteNameProvider.GetName()
        {
            return Name;
        }
    }
}
