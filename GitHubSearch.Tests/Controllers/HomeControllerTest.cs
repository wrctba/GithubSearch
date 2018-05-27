using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitHubSearch;
using GitHubSearch.Controllers;
using GitHubSearch.Models;

namespace GitHubSearch.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Search));
            Assert.IsNotNull(((Search)result.Model).Languages);
            Assert.IsTrue(((Search)result.Model).Languages.Count > 0);
        }
    }
}
