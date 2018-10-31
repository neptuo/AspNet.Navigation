using Neptuo;
using Neptuo.Activators;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Navigation.TestsApp.Wpf.ViewModels.Commands
{
    public class NavigateCommand : AsyncCommand
    {
        private readonly INavigator navigator;
        private readonly IFactory<object> ruleFactory;

        public NavigateCommand(INavigator navigator, object rule)
            : this(navigator, Factory.Instance(rule))
        { }

        public NavigateCommand(INavigator navigator, IFactory<object> ruleFactory)
        {
            Ensure.NotNull(navigator, "navigator");
            Ensure.NotNull(ruleFactory, "ruleFactory");
            this.navigator = navigator;
            this.ruleFactory = ruleFactory;
        }

        protected override bool CanExecuteOverride()
            => true;

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            object rule = ruleFactory.Create();
            if (rule != null)
            {
                // TODO: Await or not to await. It depends on "multi window mode".
                await navigator.OpenAsync(rule);
            }
        }
    }
}
