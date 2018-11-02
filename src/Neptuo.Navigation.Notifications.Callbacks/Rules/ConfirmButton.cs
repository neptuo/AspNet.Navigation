using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Rules
{
    /// <summary>
    /// An enumeration of buttons for <see cref="Confirm"/>.
    /// </summary>
    public enum ConfirmButton
    {
        /// <summary>
        /// A "Yes" or "No" buttons.
        /// </summary>
        YesNo,

        /// <summary>
        /// A three state "Yes", "No" or "Cancel" buttons.
        /// </summary>
        YesNoCancel
    }
}
