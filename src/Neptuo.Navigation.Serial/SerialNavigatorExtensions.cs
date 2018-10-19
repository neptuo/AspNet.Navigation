using Neptuo.Features;
using Neptuo.Navigation.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation
{
    /// <summary>
    /// A serial navigation extensions.
    /// </summary>
    public static class SerialNavigatorExtensions
    {
        /// <summary>
        /// Opens a view associated with the <paramref name="rule"/> rule.
        /// </summary>
        /// <param name="navigator">A navigator.</param>
        /// <param name="rule">A navigation rule.</param>
        public static void Open(this INavigator navigator, object rule)
        {
            Ensure.NotNull(navigator, "navigator");
            navigator.Features.With<ISerialNavigator>().Open(rule);
        }
    }
}
