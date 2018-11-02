using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Rules
{
    /// <summary>
    /// A result of <see cref="Confirm"/>.
    /// </summary>
    public enum ConfirmResult
    {
        /// <summary>
        /// An "Yes" result.
        /// </summary>
        Yes,

        /// <summary>
        /// A "No" result.
        /// </summary>
        No,

        /// <summary>
        /// A "Cancel" result.
        /// </summary>
        Cancel
    }
}
