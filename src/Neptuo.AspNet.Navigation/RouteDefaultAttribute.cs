using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// Defines a default key-value for the route.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RouteDefaultAttribute : Attribute, IRouteDefaultProvider
    {
        /// <summary>
        /// Gets the key of the route values parameter.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the value of the route values parameter.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="value"/> associated with <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of the route values parameter.</param>
        /// <param name="value">The value of the route values parameter.</param>
        public RouteDefaultAttribute(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            Key = key;
            Value = value;
        }

        IEnumerable<KeyValuePair<string, object>> IRouteDefaultProvider.GetUrlDefaults()
        {
            return new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>(Key, Value)
            };
        }
    }
}
