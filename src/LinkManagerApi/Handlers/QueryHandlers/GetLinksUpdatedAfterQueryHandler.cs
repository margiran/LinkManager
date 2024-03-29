using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Dtos;
using LinkManagerApi.Queries;
using LinkManagerApi.Services;
using MediatR;

namespace LinkManagerApi.Handlers.QueryHandlers;

public class GetLinksUpdatedAfterQueryHandler : IRequestHandler<GetLinksUpdatedAfterQuery, IEnumerable<LinkResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly ILinkService _linkService;

    public GetLinksUpdatedAfterQueryHandler(IMapper mapper, ILinkService linkService)
    {
        _mapper=mapper;
        _linkService=linkService;
    }
    public async Task<IEnumerable<LinkResponseDto>> Handle(GetLinksUpdatedAfterQuery request, CancellationToken cancellationToken)
    {
        var links = await _linkService.GetUpdatedAfter(request.UpdateAt);
        if (links==null)
            return null;
        return _mapper.Map<IEnumerable<LinkResponseDto>>(links);
    }
}
