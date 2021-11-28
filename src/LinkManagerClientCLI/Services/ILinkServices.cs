using LinkManagerClientCLI.Api.ApiResponse;
using LinkManagerClientCLI.Domains;

namespace LinkManagerClientCLI.Services
{
    public interface ILinkServices
    {
        Task< ICollection<LinkSearchResponse>> SearchLinks(LinkSearchRequest request);
    }
}