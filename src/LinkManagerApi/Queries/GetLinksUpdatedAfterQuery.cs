using System;
using System.Collections.Generic;
using LinkManagerApi.Contracts.V1.Responses;
using MediatR;

namespace LinkManagerApi.Queries;

public class GetLinksUpdatedAfterQuery:IRequest<IEnumerable<LinkResponse>>
{
    public DateTimeOffset UpdateAt { get; }
    
    public GetLinksUpdatedAfterQuery(DateTimeOffset updateAt)
    {
        UpdateAt=updateAt;
    }
}