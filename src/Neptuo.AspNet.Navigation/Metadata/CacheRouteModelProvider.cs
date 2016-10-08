using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    /// <summary>
    /// The caching implemntation of the <see cref="IRouteModelProvider"/>.
    /// Uses inner provider for the first time model creation, than every access for the same type is returned from the cache.
    /// </summary>
    public class CacheRouteModelProvider : IRouteModelProvider
    {
        private readonly object storageLock = new object();
        private readonly Dictionary<Type, IRouteModel> storage = new Dictionary<Type, IRouteModel>();

        private readonly IRouteModelProvider inner;

        /// <summary>
        /// Creates new instance with <paramref name="inner"/> for the first time model creation.
        /// </summary>
        /// <param name="inner">The provider for the first time model creation.</param>
        public CacheRouteModelProvider(IRouteModelProvider inner)
        {
            if (inner == null)
                throw new ArgumentNullException("inner");

            this.inner = inner;
        }

        public IRouteModel Get(Type modelType)
        {
            IRouteModel model;
            if (storage.TryGetValue(modelType, out model))
                return model;

            lock (storageLock)
            {
                if (storage.TryGetValue(modelType, out model))
                    return model;

                model = inner.Get(modelType);
                storage[modelType] = model;
                return model;
            }
        }
    }
}
