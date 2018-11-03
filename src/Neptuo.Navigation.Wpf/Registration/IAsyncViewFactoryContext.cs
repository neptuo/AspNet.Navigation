using Neptuo.Navigation.Rules;
using Neptuo.Navigation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Registration
{
    /// <summary>
    /// A context of a view factory.
    /// </summary>
    /// <typeparam name="TRule">A type of the rule.</typeparam>
    public interface IAsyncViewFactoryContext<TRule>
    {
        /// <summary>
        /// Gets a context of a view.
        /// </summary>
        IAsyncViewContext ViewContext { get; }

        /// <summary>
        /// Gets a navigator for new view.
        /// </summary>
        INavigator Navigator { get; }

        /// <summary>
        /// Gets a navigation rule that invoked current view creation.
        /// </summary>
        TRule Rule { get; }
    }

    /// <summary>
    /// A context of a view factory that provides result.
    /// </summary>
    /// <typeparam name="TRule">A type of the navigation rule.</typeparam>
    /// <typeparam name="TResult">A type of the view result.</typeparam>
    public interface IAsyncViewFactoryContext<TRule, TResult>
        where TRule : IAsyncRule<TResult>
    {
        /// <summary>
        /// Gets a context of a view.
        /// </summary>
        IAsyncViewContext<TResult> ViewContext { get; }

        /// <summary>
        /// Gets a navigator for new view.
        /// </summary>
        INavigator Navigator { get; }

        /// <summary>
        /// Gets a navigation rule that invoked current view creation.
        /// </summary>
        TRule Rule { get; }
    }
}
