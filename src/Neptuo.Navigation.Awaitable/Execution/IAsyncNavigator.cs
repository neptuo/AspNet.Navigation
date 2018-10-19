using Neptuo.Navigation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Execution
{
    /// <summary>
    /// An awaitable result providing navigation service.
    /// </summary>
    public interface IAsyncNavigator
    {
        /// <summary>
        /// Opens view associated with <paramref name="navigation"/> and returns continuation task that is resolved when the view is closed.
        /// </summary>
        /// <param name="navigation">A navigation rule.</param>
        /// <returns>A continuation task that is resolved when the view is closed.</returns>
        Task OpenAsync(object navigation);

        /// <summary>
        /// Opens view associated with <paramref name="navigation"/> and returns continuation task that is resolved when the view is closed and provides a result of the view.
        /// </summary>
        /// <param name="navigation">A navigation rule.</param>
        /// <returns>A continuation task that is resolved when the view is closed and provides a result of the view.</returns>
        Task<TResult> OpenAsync<TResult>(IAsyncRuleResult<TResult> navigation);
    }
}
