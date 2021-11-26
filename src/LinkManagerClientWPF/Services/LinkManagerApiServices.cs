
using LinkManagerClientWPF.Api;
using LinkManagerClientWPF.Api.ApiRequest;
using LinkManagerClientWPF.Api.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Services
{
    public class LinkManagerApiServices : ILinkManagerApiServices
    {
        private readonly ILinksApi _linksApi;

        public LinkManagerApiServices(ILinksApi linksApi)
        {
            _linksApi=linksApi;
        }

        public async Task< ICollection<LinkSearchResponse>> SearchLinks(LinkSearchRequest request)
        {
            Console.WriteLine(request.UpdateAt);
            var result= await _linksApi.SearchUpdateLinks(request.UpdateAt);
            return result.ToList() ;
        }
    }
}