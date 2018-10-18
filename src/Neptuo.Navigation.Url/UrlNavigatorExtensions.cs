using Neptuo.Features;
using Neptuo.Navigation.Execution;
using System;

namespace Neptuo.Navigation
{
    /// <summary>
    /// An extensions for URL generation.
    /// </summary>
    public static class UrlNavigatorExtensions
    {
        /// <summary>
        /// Gets an url associated with the <paramref name="navigation"/> rule.
        /// </summary>
        /// <param name="navigator">A navigator.</param>
        /// <param name="navigation">A navigation rule.</param>
        /// <returns>An url associated with <paramref name="navigation"/> rule.</returns>
        public static string Url(this INavigator navigator, object navigation)
        {
            Ensure.NotNull(navigator, "navigator");
            return navigator.Features.With<IUrlNavigator>().Url(navigation);
        }
    }
}
