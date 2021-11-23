using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Models
{
    public class LinkManagerModel
    {
        private LinkListModel LinkList { get; }
        public LinkManagerModel()
        {
            LinkList= new LinkListModel();
        }
        public IEnumerable<LinkModel> GetLinks(string filter = "", bool sortByVisitedCount = true)
        {
           return LinkList.GetLinks(filter,sortByVisitedCount);
        }

    }
}