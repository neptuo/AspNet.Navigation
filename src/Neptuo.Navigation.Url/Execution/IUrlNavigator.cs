using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Execution
{
    /// <summary>
    /// An URL providing navigation service.
    /// </summary>
    public interface IUrlNavigator
    {
        /// <summary>
        /// Gets an url associated with the <paramref name="rule"/> rule.
        /// </summary>
        /// <param name="rule">A navigation rule.</param>
        /// <returns>An url associated with <paramref name="rule"/> rule.</returns>
        string Url(object rule);
    }
}
