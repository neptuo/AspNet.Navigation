﻿using Neptuo.Navigation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Execution
{
    /// <summary>
    /// A navigation service accepting on closed callbacks.
    /// </summary>
    public interface ICallbackNavigator
    {
        /// <summary>
        /// Opens a view associated view <paramref name="navigation"/> rule.
        /// </summary>
        /// <param name="navigation">A navigation rule.</param>
        /// <param name="onClosed">A callback invoked after view is closed.</param>
        void Open(object navigation, Action onClosed);
        
        /// <summary>
        /// Opens a view associated view <paramref name="navigation"/> rule.
        /// </summary>
        /// <param name="navigation">A navigation rule.</param>
        /// <param name="onClosed">A callback invoked after view is closed.</param>
        /// <typeparam name="T">A type of the view result.</typeparam>
        void Open<T>(ICallbackRule<T> navigation, Action<T> onClosed);
    }
}
