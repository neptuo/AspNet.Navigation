using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Neptuo.AspNet.Navigation;
using Neptuo.AspNet.Navigation.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace System.Web.Mvc
{
    [TestClass]
    public class TestUrlExtensions
    {
        [TestMethod]
        [TestCategory("Extensions.Url")]
        public void BlogPost()
        {
            Execute("/blog/2016/10/13/first-post", new BlogPostRoute(DateTime.Now, "first-post"));
        }

        [TestMethod]
        [TestCategory("Extensions.Url")]
        public void MvcDefault()
        {
            Execute("/", new MvcRoute("Home", "Index"));
        }

        [TestMethod]
        [TestCategory("Extensions.Url")]
        public void MvcWithId()
        {
            Execute("/Home/Index/6", new MvcRoute("Home", "Index", 6));
        }


        public const string Origin = "http://localhost";

        private RequestContext PrepareRequestContext(string supportedPath)
        {
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(x => x.ApplicationPath).Returns("/");
            request.SetupGet(x => x.Url).Returns(new Uri(Origin, UriKind.Absolute));
            request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

            var workerRequest = new Mock<HttpWorkerRequest>(MockBehavior.Strict);
            workerRequest.Setup(x => x.GetServerVariable("IIS_UrlRewriteModule")).Returns((string)null);

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response.Setup(x => x.ApplyAppPathModifier(supportedPath)).Returns<string>(path => Origin + path);

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);
            context.Setup(x => x.GetService(typeof(HttpWorkerRequest))).Returns(workerRequest.Object);

            return new RequestContext(context.Object, new RouteData());
        }

        private void Execute<T>(string path, T route)
        {
            RequestContext requestContext = PrepareRequestContext(path);

            RouteModel.SetProvider(new ReflectionRouteModelProvider());
            RouteCollection routes = new RouteCollection();
            routes.MapModel<T>();

            UrlHelper helper = new UrlHelper(requestContext, routes);
            string url = helper.ModelUrl(route);
            Assert.AreEqual(Origin + path, url);
        }
    }
}
