using Neptuo;
using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// <see cref="IRouteNameProvider"/> and <see cref="IRouteUrlProvider"/> mixed in the single attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ProjectRouteAttribute : Attribute, IRouteNameProvider, IRouteUrlProvider, IRouteDefaultProvider
    {
        private readonly string name;
        private readonly string url;

        public ProjectRouteAttribute(string name, string url)
        {
            this.name = name;
            this.url = url;
        }

        public string GetName()
        {
            return name;
        }

        public string GetUrl()
        {
            return url;
        }

        public IEnumerable<KeyValuePair<string, object>> GetKeyValues()
        {
            Dictionary<string, object> defaults = new Dictionary<string, object>();
            defaults["Controller"] = "Home";
            defaults["Action"] = "Project";
            return defaults;
        }
    }
}
