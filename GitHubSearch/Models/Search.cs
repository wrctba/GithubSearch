using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GitHubSearch.Models
{
    public class Search
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public List<Item> Items { get; set; }

        [Display(Name = "ASP")]
        public bool Asp { get; set; }
        [Display(Name = "Java")]
        public bool Java { get; set; }
        [Display(Name = "Python")]
        public bool Python { get; set; }
        [Display(Name = "PHP")]
        public bool Php { get; set; }
        [Display(Name = "C")]
        public bool C { get; set; }

        
    }
}