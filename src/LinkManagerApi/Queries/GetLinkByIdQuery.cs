using System;
using LinkManagerApi.cache;
using LinkManagerApi.Dtos;
using MediatR;

namespace LinkManagerApi.Queries;

public class GetLinkByIdQuery : IRequest<LinkResponseDto>,ICacheable
{
    public Guid Id { get; }

    string ICacheable.CacheKey => $"GetLinkById->{Id}";
    public GetLinkByIdQuery(Guid id)
    {
        Id=id;
    }
}
