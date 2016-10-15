using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// The reflection-based implementation of the <see cref="IRouteModelProvider"/>.
    /// </summary>
    public class ReflectionRouteModelProvider : IRouteModelProvider
    {
        public IRouteModel Get(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException("modelType");

            bool hasName = false;
            string name = null;
            bool hasUrl = false;
            string url = null;
            Dictionary<string, object> defaults = new Dictionary<string, object>();

            foreach (Attribute attribute in modelType.GetCustomAttributes(true))
            {
                IRouteNameProvider nameProvider = attribute as IRouteNameProvider;
                if (nameProvider != null)
                {
                    if (hasName)
                        throw new InvalidOperationException(String.Format("Type route model '{0}' has defined more than one name using '{1}'.", modelType.FullName, nameProvider.GetType().FullName));
                    else
                        name = nameProvider.GetName();

                    hasName = true;
                }

                IRouteUrlProvider urlProvider = attribute as IRouteUrlProvider;
                if (urlProvider != null)
                {
                    if (hasUrl)
                        throw new InvalidOperationException(String.Format("Type route model '{0}' has defined more than one URL using '{1}'.", modelType.FullName, urlProvider.GetType().FullName));
                    else
                        url = urlProvider.GetUrl();

                    hasUrl = true;
                }

                IRouteDefaultProvider defaultProvider = attribute as IRouteDefaultProvider;
                if (defaultProvider != null)
                {
                    IEnumerable<KeyValuePair<string, object>> items = defaultProvider.GetUrlDefaults();
                    if (items != null)
                    {
                        foreach (KeyValuePair<string, object> item in items)
                            defaults.Add(item.Key, item.Value);
                    }
                }
            }

            return new DefaultRouteModel()
            {
                Name = name,
                Url = url,
                Defaults = defaults
            };
        }
    }
}
