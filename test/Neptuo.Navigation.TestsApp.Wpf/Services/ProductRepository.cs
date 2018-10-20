using Neptuo.Navigation.TestsApp.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.TestsApp.Wpf.Services
{
    public class ProductRepository
    {
        private Dictionary<Guid, Product> storage = new Dictionary<Guid, Product>();

        public IReadOnlyCollection<Product> GetList() 
            => storage.Values;

        public Product Find(Guid id)
        {
            if (storage.TryGetValue(id, out Product product))
                return product;

            return null;
        }

        public ProductBuilder Add()
            => new ProductBuilder(storage);

        public ProductBuilder Update(Guid id)
            => new ProductBuilder(storage, storage[id]);

        public void Delete(Guid id)
            => storage.Remove(id);
    }
}
