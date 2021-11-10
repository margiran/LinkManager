using LinkManagerApi.Contracts.V1.Requests;
using LinkManagerApi.Contracts.V1.Responses;
using MediatR;

namespace LinkManagerApi.Commands;

public class CreateLinkCommand :CreateLinkRequest, IRequest<LinkResponse>
{
    
}
