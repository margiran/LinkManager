using LinkManagerClientWPF.Services;
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
        public LinkManagerModel(LinkListModel linkListModel)
        {

            LinkList = linkListModel;
        }
        public IEnumerable<LinkModel> GetLinks(string filter = "")
        {
           return LinkList.GetLinks(filter);
        }


        public void AddVisitedCount(Guid id)
        {
            LinkList.AddVisitedCount(id);
        }
        public void UpdateLocalDb()
        {
            LinkList.UpdateLocalDb();
        }

    }
}