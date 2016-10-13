using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation
{
    /// <summary>
    /// Named where name is taken from class name, without defaults.
    /// </summary>
    [ProjectRoute("Project", "project/{name}")]
    public class ProjectRoute
    {
        public string Name { get; private set; }

        public ProjectRoute(string name)
        {
            Name = name;
        }
    }
}
