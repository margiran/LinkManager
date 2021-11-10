using System.Collections.Generic;
using LinkManagerApi.Dtos;
using MediatR;

namespace LinkManagerApi.Queries;

public class GetAllLinksQuery: IRequest<IEnumerable< LinkResponseDto >>
{
    public string UpdateAt { get; }
    
    public GetAllLinksQuery(string updateAt)
    {
        UpdateAt=updateAt;
    }
}