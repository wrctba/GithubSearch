using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubSearch.Models
{
    public class Language
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
    }
}