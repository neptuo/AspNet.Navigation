using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// The default implementation of the <see cref="IRouteModel"/>.
    /// </summary>
    public class DefaultRouteModel : IRouteModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public IDictionary<string, object> Defaults { get; set; }
        public IDictionary<string, object> Constraints { get; set; }
    }
}
