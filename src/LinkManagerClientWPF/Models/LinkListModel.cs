using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Models
{
    internal class LinkListModel
    {
        private bool SortByVistedCount { get; }

        private readonly List<LinkModel> Links;

        public LinkListModel(bool sortByVistedCount)
        {
            SortByVistedCount = sortByVistedCount;

            Links= new List<LinkModel>();   
        }

        public IEnumerable<LinkModel> GetLinks(string filter="")
        {
            if (string.IsNullOrEmpty(filter))
                return Links;

            return Links.Where(l => l.Argument.Contains(filter) || l.ShortDescription.Contains(filter) || l.Description.Contains(filter));
        }
    }
}
