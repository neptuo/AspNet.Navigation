using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Rules
{
    /// <summary>
    /// A navigation rule asking an user a question.
    /// </summary>
    public class Confirm : IAsyncRule<ConfirmResult>
    {
        /// <summary>
        /// Gets a notification title (not required).
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets a notification text.
        /// </summary>
        public string Text { get; }
    }
}
