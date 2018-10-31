using Neptuo;
using Neptuo.Navigation.TestsApp.Wpf.Models;
using Neptuo.Navigation.TestsApp.Wpf.Services;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels.Commands;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels.Rules;
using Neptuo.Observables;
using Neptuo.Observables.Collections;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neptuo.Navigation.TestsApp.Wpf.ViewModels
{
    public class MainViewModel : ObservableModel
    {
        private readonly INavigator navigator;
        private readonly ProductRepository productRepository;

        public ObservableCollection<Product> SelectedProducts { get; }
        public ICommand SelectProduct { get; }

        public ICommand OpenMain { get; }
        public ICommand OpenOther { get; }

        public MainViewModel(INavigator navigator, ProductRepository productRepository)
        {
            Ensure.NotNull(navigator, "navigator");
            Ensure.NotNull(productRepository, "productRepository");
            this.navigator = navigator;
            this.productRepository = productRepository;

            SelectedProducts = new ObservableCollection<Product>();
            SelectProduct = new DelegateCommand(OnSelectProduct);

            OpenMain = new NavigateCommand(navigator, new Main());
            OpenOther = new NavigateCommand(navigator, new Other());
        }

        private async void OnSelectProduct()
        {
            SelectedProducts.Clear();

            List<Guid> productIds = await navigator.OpenAsync(new ProductList());
            foreach (Guid productId in productIds)
            {
                Product product = productRepository.Find(productId);
                if (product != null)
                    SelectedProducts.Add(product);
            }
        }
    }
}
