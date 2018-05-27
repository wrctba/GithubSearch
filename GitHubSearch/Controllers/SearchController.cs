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
        private const string _TOKEN = "bf89e7f407bd5cdb12205b0b3160af08760d6110";

        SearchDAO searchDAO = new SearchDAO();
        ItemDAO itemDAO = new ItemDAO();
        LanguageDAO languageDAO = new LanguageDAO();

        [HttpPost]
        public ActionResult Index([Bind] Models.Search search)
        {
            if (ModelState.IsValid && search.Languages != null)
            {
                int count = 0;
                foreach (Language lang in search.Languages)
                {
                    if (lang.IsSelected)
                    {
                        count++;
                        Item item = GetItem(lang.Name);
                        if (item != null)
                        {
                            if (search.Items == null)
                            {
                                search.Items = new List<Item>();
                            }
                            search.Items.Add(item);
                        }
                    }
                }
                if (count >= 1) { 
                    searchDAO.Add(search);
                    return RedirectToAction("Detail", "Search", new { id = search.Id });
                }
            }
            return RedirectToAction("Index", "Home"); ;
        }

        private Item GetItem(string lang)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _TOKEN);
            client.DefaultRequestHeaders.Add("User-Agent", "GitHubSearch");

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
                    return item;
                }
            }
            return null;
        }

        public ActionResult List(int? page)
        {
            ViewBag.Languages = languageDAO.GetAll(true);
            int pagenumber = (page == null ? 1 : (page <= 0 ? 1 : (int)page));
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