using LinkManagerClientWPF.Api.ApiResponse;
using LinkManagerClientWPF.Common;
using LinkManagerClientWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Models
{
    public class LinkListModel
    {


        //private readonly List<LinkModel> Links;
        private readonly ILinkLocalDbService _localDbService;
        private readonly ILinkManagerApiServices _linkManagerApiService;

        public LinkListModel(ILinkLocalDbService localDbService, ILinkManagerApiServices linkManagerApiService)
        {
            _localDbService=localDbService;
            _linkManagerApiService=linkManagerApiService;
            //Links = new List<LinkModel>();

        }

        internal void AddVisitedCount(Guid id)
        {
            _localDbService.SetVisited(id);
        }

        public void UpdateLocalDb()
        {
            ICollection<LinkSearchResponse> apiUpdateLinks = Task.Run(() => _linkManagerApiService.SearchLinks(new Api.ApiRequest.LinkSearchRequest { UpdateAt = _localDbService.GetLastUpdated() })).Result;
            _localDbService.AddOrUpdate(apiUpdateLinks.Select(l => l.ToLink()).ToList());
            //Links.Clear();  
            //Links.AddRange(apiUpdateLinks.Select(m => new LinkModel
            //{
            //    Title = m.Title,
            //    ShortDescription = m.ShortDescription,
            //    Argument = m.Argument,
            //    DefaultPassword = m.DefaultPassword,
            //    Description = m.Description,
            //    FileName = m.FileName,
            //    Id = m.Id,
            //    InternetNeeded = m.InternetNeeded,
            //    Order = m.Order,
            //    UserName = m.UserName

            //}).ToList());
        }

        public IEnumerable<LinkModel> GetLinks(string filter="")
        {

            return _localDbService.GetAll(filter);
            //IEnumerable<LinkModel> links;

            //if (string.IsNullOrEmpty(filter))
            //    links = Links;
            //else
            //    links = Links.Where(l => l.Argument.Contains(filter) || l.ShortDescription.Contains(filter));

            //if (sortByVisitedCount)
            //    return links.OrderByDescending(l => l.LinkVisitCount);
            //return links.OrderBy(l => l.Order);
        }
    }
}
