using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Models
{
    internal class LinkManagerModel
    {
        public LinkListModel LinkList { get; }
        public LinkManagerModel()
        {
            LinkList= new LinkListModel(true);
        }
        public IEnumerable<LinkModel> GetLinks(string filter = "")
        {
           return LinkList.GetLinks(filter);
        }

    }
}