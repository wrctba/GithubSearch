using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubSearch.Models
{
    public class BaseDAO
    {
        protected string _connectionString;
        public BaseDAO()
        {
            _connectionString = string.Format(System.Configuration.ConfigurationManager.ConnectionStrings["GithubSearchDB"].ToString());
        }
    }
}