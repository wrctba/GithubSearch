using System.Web.Mvc;
using GitHubSearch.Models;

namespace GitHubSearch.Controllers
{
    public class HomeController : Controller
    {

        LanguageDAO languageDAO = new LanguageDAO();

        public ActionResult Index()
        {
            Search search = new Search();
            search.Languages = languageDAO.GetAll(true);
            return View(search);
        }
    }
}