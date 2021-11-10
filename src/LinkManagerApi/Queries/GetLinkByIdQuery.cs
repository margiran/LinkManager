using System;
using LinkManagerApi.Dtos;
using MediatR;

namespace LinkManagerApi.Queries;

public class GetLinkByIdQuery : IRequest<LinkResponseDto>
{
    public Guid Id { get; }
    
    public GetLinkByIdQuery(Guid id)
    {
        Id=id;
    }
}
