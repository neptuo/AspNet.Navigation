using Neptuo;
using Neptuo.Features;
using Neptuo.Navigation.Execution;
using Neptuo.Navigation.Rules;
using Neptuo.Navigation.TestsApp.Wpf.Services;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels.Rules;
using Neptuo.Navigation.TestsApp.Wpf.Views;
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

            ProductRepository = new ProductRepository();
            ProductRepository.Create().Name("Hotel A").UnitPrice(200).Save();

            Navigator = new Navigator(new FeatureObject(new AsyncNavigator(this)));
            Navigator.OpenAsync(new Main());
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

    class AsyncNavigator : IAsyncNavigator
    {
        private readonly App app;

        private ViewContext<bool> main;
        private ViewContext<bool> other;
        private ViewContext<List<Guid>> productList;

        public AsyncNavigator(App app)
        {
            Ensure.NotNull(app, "app");
            this.app = app;
        }

        public Task OpenAsync(object rule)
        {
            Ensure.NotNull(rule, "rule");

            if (rule is Main)
                return OpenRule(ref main, () => new MainWindow(new MainViewModel(app.Navigator, app.ProductRepository)));
            else if (rule is Other)
                return OpenRule(ref other, () => new OtherWindow());
            else if (rule is ProductList)
                return OpenRule(ref productList, () => new ProductListWindow(new ProductListViewModel(app.ProductRepository)), true);

            throw Ensure.Exception.InvalidOperation($"Missing handler for rule '{rule.GetType().Name}'.");
        }

        private Task<T> OpenRule<T>(ref ViewContext<T> context, Func<Window> windowFactory, bool isModal = false)
        {
            if (context == null)
            {
                var window = windowFactory();

                window.Closed += OnClosed;
                window.Show();

                context = new ViewContext<T>(window);

                if (isModal)
                {
                    context.IsModal = true;
                    if (main != null)
                        main.Window.IsEnabled = false;
                }
            }
            else
            {
                context.Window.Activate();
            }

            return context.TaskSource.Task;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Window wnd = (Window)sender;

            void Clear<T>(ref ViewContext<T> context, T result = default(T))
            {
                context.Window.Closed -= OnClosed;
                context.OnClose(result);
                if (context.IsModal)
                {
                    if (main != null)
                        main.Window.IsEnabled = true;
                }

                context = null;
            }

            if (main != null && wnd == main.Window)
                Clear(ref main);
            else if (other != null && wnd == other.Window)
                Clear(ref other);
            else if (productList != null && wnd == productList.Window)
                Clear(ref productList, new List<Guid>() { app.ProductRepository.GetList().First().Id });
        }

        public Task<TResult> OpenAsync<TResult>(IAsyncRule<TResult> rule)
        {
            if (rule is ProductList)
                return (Task<TResult>)(object)OpenRule(ref productList, () => new ProductListWindow(new ProductListViewModel(app.ProductRepository)), true);

            throw Ensure.Exception.InvalidOperation($"Missing handler for rule '{rule.GetType().Name}'.");
        }

        class ViewContext<T>
        {
            public Window Window { get; }
            public TaskCompletionSource<T> TaskSource { get; }
            public bool IsModal { get; set; }

            public ViewContext(Window window)
            {
                Window = window;
                TaskSource = new TaskCompletionSource<T>();
            }

            public void OnClose(T result)
            {
                TaskSource.TrySetResult(result);
            }
        }
    }
}
