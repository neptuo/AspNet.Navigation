using Neptuo;
using Neptuo.Navigation.TestsApp.Wpf.Models;
using Neptuo.Navigation.TestsApp.Wpf.Services;
using Neptuo.Observables;
using Neptuo.Observables.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.TestsApp.Wpf.ViewModels
{
    public class ProductListViewModel : ObservableModel
    {
        private readonly ProductRepository repository;

        public ObservableCollection<Product> Items { get; }

        public ProductListViewModel(ProductRepository repository)
        {
            Ensure.NotNull(repository, "repository");
            this.repository = repository;

            Items = new ObservableCollection<Product>(repository.GetList());
        }
    }
}
