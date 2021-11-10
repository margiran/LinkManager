using LinkManagerApi.Contracts.V1.Requests;
using LinkManagerApi.Contracts.V1.Responses;
using MediatR;

namespace LinkManagerApi.Commands;

public class UpdateLinkCommand :UpdateLinkRequest, IRequest<LinkResponse>
{
    
}