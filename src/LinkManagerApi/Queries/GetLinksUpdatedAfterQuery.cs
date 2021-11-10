using System;
using System.Collections.Generic;
using LinkManagerApi.Dtos;
using MediatR;

namespace LinkManagerApi.Queries;

public class GetLinksUpdatedAfterQuery:IRequest<IEnumerable<LinkResponseDto>>
{
    public DateTimeOffset UpdateAt { get; }
    
    public GetLinksUpdatedAfterQuery(DateTimeOffset updateAt)
    {
        UpdateAt=updateAt;
    }
}