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

    class AsyncNavigator : IAsyncNavigator
    {
        private readonly App app;
        private readonly List<ViewRule> factories = new List<ViewRule>();
        private readonly List<ViewContext> views = new List<ViewContext>();

        public AsyncNavigator(App app)
        {
            Ensure.NotNull(app, "app");
            this.app = app;
        }

        public void Add<TRule, TResult>(Func<TRule, IViewContext<TResult>, Window> factory, bool isModal = false)
            where TRule : IAsyncRule<TResult>
        {
            factories.Add(new ViewRule<TRule, TResult>(factory, isModal));
        }

        public void Add<TRule>(Func<TRule, Window> factory, bool isModal = false)
        {
            factories.Add(new ViewRule<TRule>(factory, isModal));
        }

        public Task OpenAsync(object rule)
        {
            Ensure.NotNull(rule, "rule");

            Type ruleType = rule.GetType();
            foreach (ViewRule registration in factories)
            {
                if (registration.RuleType.IsAssignableFrom(ruleType))
                    return OpenRule<bool>(rule, registration);
            }

            //if (rule is Main)
            //    return OpenRule(context => new MainWindow(new MainViewModel(app.Navigator, app.ProductRepository)));
            //else if (rule is Other)
            //    return OpenRule(context => new OtherWindow());
            //else if (rule is ProductList)
            //    return OpenRule(context => new ProductListWindow(new ProductListViewModel(app.ProductRepository), context), true);

            throw Ensure.Exception.InvalidOperation($"Missing handler for rule '{rule.GetType().Name}'.");
        }

        public Task<TResult> OpenAsync<TResult>(IAsyncRule<TResult> rule)
        {
            Ensure.NotNull(rule, "rule");

            Type ruleType = rule.GetType();
            foreach (ViewRule registration in factories)
            {
                if (registration.RuleType.IsAssignableFrom(ruleType))
                    return OpenRule<TResult>(rule, registration);
            }

            //if (rule is ProductList)
            //    return (Task<TResult>)(object)OpenRule(ref productList, context => new ProductListWindow(new ProductListViewModel(app.ProductRepository), context), true);

            throw Ensure.Exception.InvalidOperation($"Missing handler for rule '{rule.GetType().Name}'.");
        }

        private Task<T> OpenRule<T>(object rule, ViewRule registration)
        {
            var context = views.OfType<ViewContext<T>>().FirstOrDefault(v => v.Registration == registration);
            if (!registration.IsModal || context == null)
            {
                context = new ViewContext<T>();
                context.Registration = registration;
                views.Add(context);

                var window = registration.Open(rule, context);
                context.Window = window;

                window.Closed += OnClosed;
                window.Show();
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

            void Clear(ViewContext context)
            {
                context.Window.Closed -= OnClosed;
                context.OnClose();

                // TODO: Modality.
                //if (context.IsModal)
                //{
                //    if (main != null)
                //        main.Window.IsEnabled = true;
                //}

                views.Remove(context);
                context = null;
            }

            ViewContext target = null;
            foreach (ViewContext view in views)
            {
                if (view.Window == wnd)
                {
                    target = view;
                    break;
                }
            }

            if (target != null)
                Clear(target);
        }

        abstract class ViewRule
        {
            public Type RuleType { get; protected set; }
            public bool IsModal { get; protected set; }

            public abstract Window Open(object rule, ViewContext viewContext);
        }

        class ViewRule<TRule> : ViewRule
        {
            private readonly Func<TRule, Window> factory;

            public ViewRule(Func<TRule, Window> factory, bool isModal)
            {
                Ensure.NotNull(factory, "factory");
                this.factory = factory;

                RuleType = typeof(TRule);
                IsModal = isModal;
            }

            public override Window Open(object rule, ViewContext viewContext)
                => OpenOverride((TRule)rule);

            protected Window OpenOverride(TRule rule)
                => factory(rule);
        }

        class ViewRule<TRule, TResult> : ViewRule
            where TRule : IAsyncRule<TResult>
        {
            private readonly Func<TRule, IViewContext<TResult>, Window> factory;

            public ViewRule(Func<TRule, IViewContext<TResult>, Window> factory, bool isModal)
            {
                Ensure.NotNull(factory, "factory");
                this.factory = factory;

                RuleType = typeof(TRule);
                IsModal = isModal;
            }

            public override Window Open(object rule, ViewContext viewContext)
                => OpenOverride((TRule)rule, (IViewContext<TResult>)viewContext);

            protected Window OpenOverride(TRule rule, IViewContext<TResult> viewContext)
                => factory(rule, viewContext);
        }

        abstract class ViewContext
        {
            public Window Window { get; set; }
            public ViewRule Registration { get; set; }
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
