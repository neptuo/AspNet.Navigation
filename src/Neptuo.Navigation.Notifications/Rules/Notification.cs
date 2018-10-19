using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Navigation.Rules
{
    /// <summary>
    /// A navigation rule showing a notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets a notification title (not required).
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets a notification text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets a notification type.
        /// </summary>
        public NotificationType Type { get; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="title">A notification title.</param>
        /// <param name="text">A notification text</param>
        /// <param name="type">A notification type.</param>
        public Notification(string title, string text, NotificationType type = NotificationType.Information)
        {
            Ensure.NotNull(text, "text");
            Title = title;
            Text = text;
            Type = type;
        }

        /// <summary>
        /// Creates a new instance without title.
        /// </summary>
        /// <param name="title">A notification title.</param>
        /// <param name="text">A notification text</param>
        /// <param name="type">A notification type.</param>
        public Notification(string text, NotificationType type = NotificationType.Information)
        {
            Ensure.NotNull(text, "text");
            Text = text;
            Type = type;
        }
    }
}
