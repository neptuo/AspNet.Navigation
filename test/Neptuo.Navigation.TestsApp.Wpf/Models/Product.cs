using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.TestsApp.Wpf.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Product(Guid id, string name, decimal unitPrice)
        {
            Ensure.NotNullOrEmpty(name, "name");
            Ensure.PositiveOrZero(unitPrice, "unitPrice");
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
