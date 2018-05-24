using System.Web.Mvc;
using GitHubSearch.Models;

namespace GitHubSearch.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View(new Search());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
    }
}