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

        public override bool Equals(Object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            Search o = (Search)obj;
            return (o.Id == this.Id);
        }

        public override int GetHashCode()
        {
            var hashCode = -1190849415;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            return hashCode;
        }
    }
}