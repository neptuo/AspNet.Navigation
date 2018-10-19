using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Execution
{
    /// <summary>
    /// A basic navigator with capability to open view.
    /// </summary>
    public interface ISerialNavigator
    {
        /// <summary>
        /// Opens a view associated with the <paramref name="rule"/> rule.
        /// </summary>
        /// <param name="rule">A navigation rule.</param>
        void Open(object rule);
    }
}
