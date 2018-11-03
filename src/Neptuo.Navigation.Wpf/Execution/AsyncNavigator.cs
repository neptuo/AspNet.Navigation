using Neptuo.Navigation.Rules;
using Neptuo.Navigation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Neptuo.Navigation.Execution
{
    public class AsyncNavigator : IAsyncNavigator
    {
        private readonly Application app;
        private readonly List<ViewRule> factories = new List<ViewRule>();
        private readonly List<ViewContext> views = new List<ViewContext>();

        public AsyncNavigator(Application app)
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
                => OpenOverride((TRule)rule, (IAsyncViewContext<TResult>)viewContext);

                => factory(rule, viewContext);
            protected Window OpenOverride(TRule rule, IAsyncViewContext<TResult> viewContext)
        }

        abstract class ViewContext
        {
            public Window Window { get; set; }
            public ViewRule Registration { get; set; }
            public bool IsModal { get; set; }

            public abstract void OnClose();
        }

        class ViewContext<T> : ViewContext, IViewContext<T>
        class ViewContext<T> : ViewContext, IAsyncViewContext<T>
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
}
