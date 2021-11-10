using System;
using LinkManagerApi.Contracts.V1.Responses;
using MediatR;

namespace LinkManagerApi.Queries;

public class GetLinkByIdQuery : IRequest<LinkResponse>
{
    public Guid Id { get; }
    
    public GetLinkByIdQuery(Guid id)
    {
        Id=id;
    }
}
