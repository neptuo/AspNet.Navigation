using Neptuo.Activators;
using Neptuo.Features;
using Neptuo.Navigation.Execution;
using Neptuo.Navigation.Registration;
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
            navigator.Add(Factory.Getter<Window, IAsyncViewFactoryContext<Main>>(CreateMain));
            navigator.Add(Factory.Getter<Window, IAsyncViewFactoryContext<Other>>(context => new OtherWindow()));
            navigator.Add(Factory.Getter<Window, IAsyncViewFactoryContext<ProductList, List<Guid>>>(context => new ProductListWindow(new ProductListViewModel(ProductRepository), context.ViewContext)), true);
            return navigator;
        }

        private Window CreateMain(IAsyncViewFactoryContext<Main> context) 
            => new MainWindow(new MainViewModel(Navigator, ProductRepository));
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
