using Neptuo.Navigation.TestsApp.Wpf.Services;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.TestsApp.Wpf.Views.DesignData
{
    internal static class ViewModelLocator
    {
        private static ProductRepository productRepository;
        public static ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository();
                    productRepository.Create().Name("Flat screen TV").UnitPrice(200).Save();
                    productRepository.Create().Name("Console A").UnitPrice(780).Save();
                    productRepository.Create().Name("New Computer").UnitPrice(9500).Save();
                }

                return productRepository;
            }
        }

        private static ProductListViewModel productListViewModel;
        public static ProductListViewModel ProductListViewModel
        {
            get
            {
                if (productListViewModel == null)
                    productListViewModel = new ProductListViewModel(ProductRepository);

                return productListViewModel;
            }
        }
    }
}
