using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubSearch.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Details { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            Item o = (Item)obj;
            return (o.Id == this.Id);
        }

        public override int GetHashCode()
        {
            var hashCode = 526956437;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Owner);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Language);
            return hashCode;
        }
    }
}