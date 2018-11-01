using Neptuo;
using Neptuo.Features;
using Neptuo.Navigation.Execution;
using Neptuo.Navigation.Rules;
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
        private readonly List<ViewRule> factories = new List<ViewRule>();
        private readonly List<ViewContext> views = new List<ViewContext>();

        public AsyncNavigator(App app)
        {
            Ensure.NotNull(app, "app");
            this.app = app;

            Add<Main>(rule => new MainWindow(new MainViewModel(app.Navigator, app.ProductRepository)));
            Add<Other>(rule => new OtherWindow());
            Add<ProductList, List<Guid>>((rule, context) => new ProductListWindow(new ProductListViewModel(app.ProductRepository), context));
        }

        public void Add<TRule, TResult>(Func<TRule, IViewContext<TResult>, Window> factory)
            where TRule : IAsyncRule<TResult>
        {

        }

        public void Add<TRule>(Func<TRule, Window> factory)
        {
            factories.Add(new ViewRule<TRule>(factory));
        }

        public Task OpenAsync(object rule)
        {
            Ensure.NotNull(rule, "rule");

            if (rule is Main)
                return OpenRule(context => new MainWindow(new MainViewModel(app.Navigator, app.ProductRepository)));
            else if (rule is Other)
                return OpenRule(context => new OtherWindow());
            else if (rule is ProductList)
                return OpenRule(context => new ProductListWindow(new ProductListViewModel(app.ProductRepository), context), true);

            throw Ensure.Exception.InvalidOperation($"Missing handler for rule '{rule.GetType().Name}'.");
        }

        private Task<T> OpenRule<T>(Func<IViewContext<T>, Window> windowFactory, bool isModal = false)
        {
            if (context == null)
            {
                context = new ViewContext<T>();

                var window = windowFactory(context);

                window.Closed += OnClosed;
                window.Show();

                context.Window = window;

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

            void Clear<T>(ref ViewContext<T> context)
            {
                context.Window.Closed -= OnClosed;
                context.OnClose();
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
                Clear(ref productList);
        }

        public Task<TResult> OpenAsync<TResult>(IAsyncRule<TResult> rule)
        {
            if (rule is ProductList)
                return (Task<TResult>)(object)OpenRule(ref productList, context => new ProductListWindow(new ProductListViewModel(app.ProductRepository), context), true);

            throw Ensure.Exception.InvalidOperation($"Missing handler for rule '{rule.GetType().Name}'.");
        }

        abstract class ViewRule
        {
            public abstract Window Open(object rule);
        }

        class ViewRule<TRule> : ViewRule
        {
            private readonly Func<TRule, Window> factory;

            public ViewRule(Func<TRule, Window> factory)
            {
                Ensure.NotNull(factory, "factory");
                this.factory = factory;
            }

            public override Window Open(object rule)
                => OpenOverride((TRule)rule);

            protected Window OpenOverride(TRule rule)
                => factory(rule);
        }

        abstract class ViewContext
        {
            public Window Window { get; set; }
            public bool IsModal { get; set; }

            public abstract void OnClose();
        }

        class ViewContext<T> : ViewContext, IViewContext<T>
        {
            public TaskCompletionSource<T> TaskSource { get; }
            public T Result { get; set; }

            public ViewContext()
            {
                TaskSource = new TaskCompletionSource<T>();
            }

            public override void OnClose()
            {
                TaskSource.TrySetResult(Result);
            }

            public void Close(T result)
            {
                Result = result;
                Window.Close();
            }
        }
    }

    public interface IViewContext<T>
    {
        void Close(T result);
    }
}
