using Neptuo.Features;
using Neptuo.Navigation.Execution;
using Neptuo.Navigation.TestsApp.Wpf.Services;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels.Rules;
using Neptuo.Navigation.TestsApp.Wpf.Views;
using Neptuo.Navigation.TestsApp.Wpf.Views.DesignData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Neptuo.Navigation.TestsApp.Wpf
{
    public partial class App : Application
    {
        public INavigator Navigator { get; private set; }
        public ProductRepository ProductRepository { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ProductRepository = ViewModelLocator.ProductRepository;
            ProductRepository.Create().Name("Hotel A").UnitPrice(200).Save();

            Navigator = new Navigator(new FeatureObject(RegisterViews(new AsyncNavigator(this))));

            Navigator.OpenAsync(new Main());
        }

        private AsyncNavigator RegisterViews(AsyncNavigator navigator)
        {
            navigator.Add<Main>(rule => new MainWindow(new MainViewModel(Navigator, ProductRepository)));
            navigator.Add<Other>(rule => new OtherWindow());
            navigator.Add<ProductList, List<Guid>>((rule, context) => new ProductListWindow(new ProductListViewModel(ProductRepository), context), true);
            return navigator;
        }
    }

    class Navigator : INavigator
    {
        public IFeatureProvider Features { get; }

        public Navigator(IFeatureProvider features)
        {
            Ensure.NotNull(features, "features");
            Features = features;
        }
    }
}
