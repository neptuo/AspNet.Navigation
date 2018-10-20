using Neptuo.Navigation.TestsApp.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.TestsApp.Wpf.Services
{
    public class ProductBuilder
    {
        private readonly Dictionary<Guid, Product> storage;

        private Guid? modelId;
        private string name;
        private decimal unitPrice;

        public ProductBuilder(Dictionary<Guid, Product> storage, Product model)
        {
            Ensure.NotNull(storage, "storage");
            this.storage = storage;
            this.modelId = model.Id;
            this.name = model.Name;
            this.unitPrice = model.UnitPrice;
        }

        public ProductBuilder(Dictionary<Guid, Product> storage)
        {
            Ensure.NotNull(storage, "storage");
            this.storage = storage;
        }

        public ProductBuilder Name(string name)
        {
            this.name = name;
            return this;
        }

        public ProductBuilder UnitPrice(decimal unitPrice)
        {
            this.unitPrice = unitPrice;
            return this;
        }

        public void Save()
        {
            Product model = new Product(modelId ?? Guid.NewGuid(), name, unitPrice);

            if (modelId != null)
                storage.Remove(modelId.Value);

            modelId = null;
            storage.Add(model.Id, model);
        }
    }
}
