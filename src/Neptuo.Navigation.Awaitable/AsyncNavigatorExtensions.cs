using Neptuo.Features;
using Neptuo.Navigation.Execution;
using Neptuo.Navigation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation
{
    /// <summary>
    /// Extensions for the awaitable result providing navigation service.
    /// </summary>
    public static class AsyncNavigatorExtensions
    {
        /// <summary>
        /// Opens view associated with <paramref name="model"/> and returns continuation task that is resolved when the view is closed.
        /// </summary>
        /// <param name="navigator">A navigator.</param>
        /// <param name="model">A navigation rule.</param>
        /// <returns>A continuation task that is resolved when the view is closed.</returns>
        public static Task OpenAsync(this INavigator navigator, object model)
        {
            Ensure.NotNull(navigator, "navigator");
            return navigator.Features.With<IAsyncNavigator>().OpenAsync(model);
        }

        /// <summary>
        /// Opens view associated with <paramref name="model"/> and returns continuation task that is resolved when the view is closed and provides a result of the view.
        /// </summary>
        /// <param name="navigator">A navigator.</param>
        /// <param name="model">A navigation rule.</param>
        /// <returns>A continuation task that is resolved when the view is closed and provides a result of the view.</returns>
        public static Task<TResult> OpenAsync<TResult>(this INavigator navigator, IAsyncRuleResult<TResult> model)
        {
            Ensure.NotNull(navigator, "navigator");
            return navigator.Features.With<IAsyncNavigator>().OpenAsync(model);
        }
    }
}
