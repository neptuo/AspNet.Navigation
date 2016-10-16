using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// Defines constraint for route URL parameter as C# regex pattern.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RouteConstraintRegexAttribute : Attribute, IRouteConstraintProvider
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// Gets the constraint of the parameter.
        /// </summary>
        public string Constraint { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="constraint">The constraint of the parameter.</param>
        public RouteConstraintRegexAttribute(string parameterName, string constraint)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName");

            if (string.IsNullOrEmpty(constraint))
                throw new ArgumentNullException("constraint");

            ParameterName = parameterName;
            Constraint = constraint;
        }

        IEnumerable<KeyValuePair<string, object>> IRouteConstraintProvider.GetConstraints()
        {
            return new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>(ParameterName, Constraint)
            };
        }
    }
}
