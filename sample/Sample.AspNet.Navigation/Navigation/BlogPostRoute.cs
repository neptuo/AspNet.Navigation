using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// Named route with defaults for controller and action.
    /// </summary>
    [RouteName("BlogPost")]
    [RouteUrl("blog/{year}/{month}/{day}/{url}")]
    [RouteController("Home", "Blog")]
    public class BlogPostRoute
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public string Url { get; private set; }

        public BlogPostRoute(DateTime publishedAt, string url)
        {
            Year = publishedAt.Year;
            Month = publishedAt.Month;
            Day = publishedAt.Day;
            Url = url;
        }
    }
}
