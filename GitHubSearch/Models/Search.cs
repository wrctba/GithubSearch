using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GitHubSearch.Models
{

    public class AtLanguageMustBeCheckedAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<Language> instance = value as List<Language>;
            int count = instance == null ? 0 : (from p in instance
                                                where p.IsSelected == true
                                                select p).Count();
            if (count >= 1)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }

    public class Search
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public List<Item> Items { get; set; }

        [AtLanguageMustBeChecked(ErrorMessage = "Please select at least one language")]
        public List<Language> Languages { get; set; }
        /*[Display(Name = "ASP")]
        public bool Asp { get; set; }
        [Display(Name = "Java")]
        public bool Java { get; set; }
        [Display(Name = "Python")]
        public bool Python { get; set; }
        [Display(Name = "PHP")]
        public bool Php { get; set; }
        [Display(Name = "C")]
        public bool C { get; set; }*/

        
    }
}