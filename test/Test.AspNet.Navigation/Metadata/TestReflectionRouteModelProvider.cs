using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AspNet.Navigation.Metadata
{
    [TestClass]
    public class TestReflectionRouteModelProvider
    {
        [TestMethod]
        [TestCategory("Metadata.Reflection")]
        public void BlogPost()
        {
            ReflectionRouteModelProvider modelProvider = new ReflectionRouteModelProvider();
            IRouteModel model = modelProvider.Get(typeof(BlogPostRoute));
            Assert.AreEqual("BlogPost", model.Name);
            Assert.AreEqual("blog/{year}/{month}/{day}/{slug}", model.Url);
            Assert.AreEqual(2, model.Defaults.Count);
            Assert.AreEqual(true, model.Defaults.ContainsKey("Controller"));
            Assert.AreEqual("Blog", model.Defaults["Controller"]);
            Assert.AreEqual(true, model.Defaults.ContainsKey("Action"));
            Assert.AreEqual("List", model.Defaults["Action"]);
        }

        [TestMethod]
        [TestCategory("Metadata.Reflection")]
        public void Mvc()
        {
            ReflectionRouteModelProvider modelProvider = new ReflectionRouteModelProvider();
            IRouteModel model = modelProvider.Get(typeof(MvcRoute));
            Assert.AreEqual(null, model.Name);
            Assert.AreEqual("{controller}/{action}/{id}", model.Url);
            Assert.AreEqual(3, model.Defaults.Count);
            Assert.AreEqual(true, model.Defaults.ContainsKey("Controller"));
            Assert.AreEqual("Home", model.Defaults["Controller"]);
            Assert.AreEqual(true, model.Defaults.ContainsKey("Action"));
            Assert.AreEqual("Index", model.Defaults["Action"]);
            Assert.AreEqual(true, model.Defaults.ContainsKey("id"));
            Assert.AreEqual(null, model.Defaults["id"]);
        }

        [TestMethod]
        [TestCategory("Metadata.Reflection")]
        public void Project()
        {
            ReflectionRouteModelProvider modelProvider = new ReflectionRouteModelProvider();
            IRouteModel model = modelProvider.Get(typeof(ProjectRoute));
            Assert.AreEqual("Project", model.Name);
            Assert.AreEqual("project/{name}", model.Url);
            Assert.AreEqual(0, model.Defaults.Count);
        }
    }
}
