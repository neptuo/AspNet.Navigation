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
        /// Opens a view paired to <paramref name="navigation"/> rule.
        /// </summary>
        /// <param name="navigation">A navigation rule.</param>
        void Open(object navigation);
    }
}
