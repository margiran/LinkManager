using LinkManagerClientWPF.Api.ApiRequest;
using LinkManagerClientWPF.Api.ApiResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkManagerClientWPF.Services
{
    public interface ILinkManagerApiServices
    {
        Task< ICollection<LinkSearchResponse>> SearchLinks(LinkSearchRequest request);
    }
}