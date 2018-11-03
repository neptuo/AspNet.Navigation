using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Views
{
    /// <summary>
    /// A context of an view.
    /// </summary>
    public interface IAsyncViewContext
    {
        /// <summary>
        /// Closes current view.
        /// </summary>
        void Close();
    }

    /// <summary>
    /// A context of an view.
    /// Provides possibility to pass a result to the caller.
    /// </summary>
    /// <typeparam name="T">A type of the result.</typeparam>
    public interface IAsyncViewContext<T>
    {
        /// <summary>
        /// Closes current view and passes back <paramref name="result"/>.
        /// </summary>
        /// <param name="result">A view result.</param>
        void Close(T result);
    }
}
