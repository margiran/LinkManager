using LinkManagerClientCLI.Api;
using LinkManagerClientCLI.Api.ApiResponse;
using LinkManagerClientCLI.Domains;

namespace LinkManagerClientCLI.Services
{
    public class LinkServices : ILinkServices
    {
        private readonly ILinksApi _linksApi;

        public LinkServices(ILinksApi linksApi)
        {
            _linksApi=linksApi;
        }

        public async Task< ICollection<LinkSearchResponse>> SearchLinks(LinkSearchRequest request)
        {
            Console.WriteLine(request.UpdateAt);
            var result= await _linksApi.SearchUpdateLinks(request.UpdateAt);
            return result.ToList();
        }
    }
}