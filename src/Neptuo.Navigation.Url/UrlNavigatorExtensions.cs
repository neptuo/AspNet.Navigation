using Neptuo.Features;
using Neptuo.Navigation.Execution;
using System;

namespace Neptuo.Navigation
{
    /// <summary>
    /// Extensions for URL generation.
    /// </summary>
    public static class UrlNavigatorExtensions
    {
        /// <summary>
        /// Gets an url associated with the <paramref name="rule"/> rule.
        /// </summary>
        /// <param name="navigator">A navigator.</param>
        /// <param name="rule">A navigation rule.</param>
        /// <returns>An url associated with <paramref name="rule"/> rule.</returns>
        public static string Url(this INavigator navigator, object rule)
        {
            Ensure.NotNull(navigator, "navigator");
            return navigator.Features.With<IUrlNavigator>().Url(rule);
        }
    }
}
