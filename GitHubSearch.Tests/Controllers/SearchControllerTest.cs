using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GitHubSearch.Controllers;
using GitHubSearch.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitHubSearch.Tests.Controllers
{
    [TestClass]
    public class SearchControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            SearchController controller = new SearchController();

            //Search whith no Language selected
            Search search = new Search();
            search.Languages = (new LanguageDAO()).GetAll();

            //Act
            RedirectToRouteResult redirect = (RedirectToRouteResult)controller.Index(search);

            // Assert
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.RouteValues["action"]);
            Assert.AreEqual("Home", redirect.RouteValues["controller"]);


            //Search whith a Language selected
            search.Languages[0].IsSelected = true;

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Index(search);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Detail", result.RouteValues["action"]);
            Assert.AreEqual("Search", result.RouteValues["controller"]);

        }

        [TestMethod]
        public void List()
        {
            // Arrange

            //Search whith a valid language
            SearchController controller = new SearchController();

            // Act
            ViewResult result = (ViewResult)controller.List(int.MinValue);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Search>));

            // Act
            result = (ViewResult)controller.List(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Search>));
        
            // Act
            result = (ViewResult)controller.List(int.MaxValue);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Search>));
        }

        [TestMethod]
        public void Detail()
        {
            // Arrange

            //Search whith a valid language
            SearchController controller = new SearchController();

            // Act
            RedirectToRouteResult redirect = (RedirectToRouteResult)controller.Detail(0);

            // Assert
            Assert.IsNotNull(redirect);
            Assert.AreEqual("List", redirect.RouteValues["action"]);
            Assert.AreEqual("Search", redirect.RouteValues["controller"]);

            // Arrange

            int count = 0;
            List<Search> list = (new SearchDAO()).GetAll(1, out count);
            Search search = list[0];
            // Act
            ViewResult result = (ViewResult)controller.Detail(search.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Search));
            Assert.AreEqual(result.Model, search);
        }

        [TestMethod]
        public void Item()
        {
            // Arrange
            SearchController controller = new SearchController();

            // Act
            RedirectToRouteResult redirect = (RedirectToRouteResult)controller.Item(0);

            // Assert
            Assert.IsNotNull(redirect);
            Assert.AreEqual("Index", redirect.RouteValues["action"]);
            Assert.AreEqual("Home", redirect.RouteValues["controller"]);

            // Arrange
            Item item = (new ItemDAO()).Get(1863329);

            // Act
            ViewResult result = (ViewResult)controller.Item(item.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Item));
            Assert.AreEqual(result.Model, item);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            SearchController controller = new SearchController();

            // Arrange
            Search search = new Search();
            search.Languages = (new LanguageDAO()).GetAll();
            search.Languages[0].IsSelected = true;
            (new SearchDAO()).Add(search);

            // Act
            RedirectToRouteResult redirect = (RedirectToRouteResult)controller.Delete(search.Id);

            // Assert
            Assert.IsNotNull(redirect);
            Assert.AreEqual("List", redirect.RouteValues["action"]);
            Assert.AreEqual("Search", redirect.RouteValues["controller"]);
        }
    }
}
