using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Models
{
    public class LinkListModel
    {

        private readonly List<LinkModel> Links;

        public LinkListModel()
        {

            Links= new List<LinkModel>();

            Links.Add(new Models.LinkModel { Title = "google", FileName = "chrome", Argument = "google.com", Id = Guid.NewGuid(), Order = 1, ShortDescription = "google" });
            Links.Add(new Models.LinkModel { Title = "yahoo", FileName = "chrome", Argument = "yahoo.com", Id = Guid.NewGuid(), Order = 2, ShortDescription = "yahoo" });

        }

        public IEnumerable<LinkModel> GetLinks(string filter="", bool sortByVisitedCount = true)
        {
            IEnumerable<LinkModel> links;

            if (string.IsNullOrEmpty(filter))
                links= Links;
            else
                 links= Links.Where(l => l.Argument.Contains(filter) || l.ShortDescription.Contains(filter));

            if (sortByVisitedCount)
                return links.OrderByDescending(l => l.LinkVisitCount);
            return links.OrderBy(l=>l.Order);
        }
    }
}
