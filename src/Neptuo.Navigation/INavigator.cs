using Neptuo.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation
{
    /// <summary>
    /// A navigation service facade.
    /// </summary>
    public interface INavigator
    {
        /// <summary>
        /// Gets a collection of navigation features, a low-level contracts for executing navigation.
        /// </summary>
        IFeatureProvider Features { get; }
    }
}
