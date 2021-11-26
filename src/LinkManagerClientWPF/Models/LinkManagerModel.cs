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
        private readonly ILinkLocalDbService _localDbService;
        private readonly ILinkManagerApiServices _linkManagerApiService;

        private LinkListModel LinkList { get; }
        public LinkManagerModel(ILinkLocalDbService localDbService, ILinkManagerApiServices linkManagerApiService)
        {
            _localDbService = localDbService;
            _linkManagerApiService = linkManagerApiService;
            
            LinkList= new LinkListModel(_localDbService,_linkManagerApiService);
        }
        public IEnumerable<LinkModel> GetLinks(string filter = "", bool sortByVisitedCount = true)
        {
           return LinkList.GetLinks(filter,sortByVisitedCount);
        }

        public void UpdateLocalDb()
        {
            LinkList.UpdateLocalDb();
        }

    }
}