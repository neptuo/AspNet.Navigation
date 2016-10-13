using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Neptuo.AspNet.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace System.Web.Mvc
{
    [TestClass]
    public class TestRouteCollectionExtensions
    {
        [TestMethod]
        [TestCategory("Extensions.RouteCollection")]
        public void BlogPost()
        {
            string year = "2016";
            string month = "10";
            string day = "13";
            string slug = "first-post";

            HttpContextBase httpContext = PrepareHttpContext("/blog/" + year + "/" + month + "/" + day + "/" + slug);

            RouteCollection routes = new RouteCollection();
            routes.MapModel<BlogPostRoute>();

            RouteData data = routes.GetRouteData(httpContext);
            Assert.IsNotNull(data);

            Assert.AreEqual(6, data.Values.Count);
            Assert.AreEqual(true, data.Values.ContainsKey("Controller"));
            Assert.AreEqual("Blog", data.Values["Controller"]);
            Assert.AreEqual(true, data.Values.ContainsKey("Action"));
            Assert.AreEqual("List", data.Values["Action"]);
            Assert.AreEqual(true, data.Values.ContainsKey("year"));
            Assert.AreEqual(year, data.Values["year"]);
            Assert.AreEqual(true, data.Values.ContainsKey("month"));
            Assert.AreEqual(month, data.Values["month"]);
            Assert.AreEqual(true, data.Values.ContainsKey("day"));
            Assert.AreEqual(day, data.Values["day"]);
            Assert.AreEqual(true, data.Values.ContainsKey("slug"));
            Assert.AreEqual(slug, data.Values["slug"]);
        }

        public const string Origin = "http://localhost";

        private HttpContextBase PrepareHttpContext(string supportedPath)
        {
            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(x => x.ApplicationPath).Returns("/");
            request.SetupGet(x => x.AppRelativeCurrentExecutionFilePath).Returns("~" + supportedPath);
            request.SetupGet(x => x.PathInfo).Returns((string)null);
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

            return context.Object;
        }
    }
}
