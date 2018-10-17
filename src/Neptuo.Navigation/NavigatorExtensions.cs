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
    /// A basic navigation extensions.
    /// </summary>
    public static class NavigatorExtensions
    {
        /// <summary>
        /// Opens a view paired to <paramref name="navigation"/> rule.
        /// </summary>
        /// <param name="navigator">A navigator.</param>
        /// <param name="navigation">A navigation rule.</param>
        public static void Open(this INavigator navigator, object navigation)
            => navigator.Features.With<ISerialNavigator>().Open(navigation);
    }
}
