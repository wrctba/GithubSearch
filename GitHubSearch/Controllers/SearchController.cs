using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using GitHubSearch.Models;
using Newtonsoft.Json.Linq;
using PagedList;

namespace GitHubSearch.Controllers
{
    public class SearchController : Controller
    {

        private const string _URL = "https://api.github.com/search/repositories";
        private const string _LANG = "?q=language:";
        private const string _SORT = "&sort=stars&order=desc";
        private const string _TOKEN = "e947df27bb3001daff3062bbf7b9fcfa64f9d766";

        SearchDAO searchDAO = new SearchDAO();
        ItemDAO itemDAO = new ItemDAO();

        [HttpPost]
        public ActionResult Index([Bind] Models.Search search)
        {

            if (search.Asp)
                AddLang("ASP", search);
            if (search.C)
                AddLang("C", search);
            if (search.Java)
                AddLang("JAVA", search);
            if (search.Php)
                AddLang("PHP", search);
            if (search.Python)
                AddLang("PYTHON", search);

            searchDAO.Add(search);
            return RedirectToAction("Detail", new { id = search.Id });
            //return View(search);
        }

        private void AddLang(string lang, GitHubSearch.Models.Search search)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _TOKEN);
            client.DefaultRequestHeaders.Add("User-Agent", "GithubSearch");

            string urlParameters = _LANG + lang + _SORT;
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                string strResult = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(strResult);
                JToken items = json.GetValue("items");
                foreach (var ret in items)
                {
                    GitHubSearch.Models.Item item = new GitHubSearch.Models.Item();
                    item.Id = ret.SelectToken("id").Value<int>();
                    item.Name = ret.SelectToken("name").Value<string>();
                    item.Owner = ret.SelectToken("owner").SelectToken("login").Value<string>();
                    item.Language = ret.SelectToken("language").Value<string>();
                    item.Description = ret.SelectToken("description").Value<string>();
                    item.Details = ret.ToString();
                    if (search.Items == null)
                        search.Items = new List<Models.Item>();
                    search.Items.Add(item);
                    break;
                }
            }
        }

        public ActionResult List(int? page)
        {
            int pagenumber = (page ?? 1);
            int count = 0;
            List<Search> list = searchDAO.GetAll(pagenumber, out count);
            IPagedList<Search> pageOrders = new StaticPagedList<Search>(list, pagenumber, SearchDAO.PageSize, count);
            return View(pageOrders);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("List", "Search");

            Search search = searchDAO.Get((int)id);
            if (search == null)
                return RedirectToAction("List", "Search");
            else
                return View(search);
        }

        public ActionResult Item(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            Item item = itemDAO.Get((int)id);
            if (item == null)
                return RedirectToAction("Index", "Home");
            else
                return View(item);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("List", "Search");

            if (searchDAO.Delete((int)id)) {
                return RedirectToAction("List", "Search");
            }
            else
                return View(searchDAO.Get((int)id));
        }
    }
}