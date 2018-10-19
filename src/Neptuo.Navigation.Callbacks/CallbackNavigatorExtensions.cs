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
    /// Extensions for passing callback after view is closed.
    /// </summary>
    public static class CallbackNavigatorExtensions
    {
        /// <summary>
        /// Opens a view associated view <paramref name="rule"/> rule.
        /// </summary>
        /// <param name="navigator">A navigator.</param>
        /// <param name="rule">A navigation rule.</param>
        /// <param name="onClosed">A callback invoked after view is closed.</param>
        public static void Open(this INavigator navigator, object rule, Action onClosed)
        {
            Ensure.NotNull(navigator, "navigator");
            navigator.Features.With<ICallbackNavigator>().Open(rule, onClosed);
        }

        /// <summary>
        /// Opens a view associated view <paramref name="rule"/> rule.
        /// </summary>
        /// <param name="navigator">A navigator.</param>
        /// <param name="rule">A navigation rule.</param>
        /// <param name="onClosed">A callback invoked after view is closed.</param>
        /// <typeparam name="T">A type of the view result.</typeparam>
        public static void Open<T>(this INavigator navigator, ICallbackRule<T> rule, Action<T> onClosed)
        {
            Ensure.NotNull(navigator, "navigator");
            navigator.Features.With<ICallbackNavigator>().Open(rule, onClosed);
        }
    }
}
