using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Contracts.V1.Responses;
using LinkManagerApi.Queries;
using LinkManagerApi.Services;
using MediatR;

namespace LinkManagerApi.Handlers.QueryHandlers;
public class GetAllLinksQueryHandler : IRequestHandler<GetAllLinksQuery, IEnumerable<LinkResponse>>
{
    private readonly IMapper _mapper;
    private readonly ILinkService _linkService;

    public GetAllLinksQueryHandler(IMapper mapper, ILinkService linkService)
    {
        _mapper = mapper;
        _linkService = linkService;
    }
    public async Task<IEnumerable<LinkResponse>> Handle(GetAllLinksQuery request, CancellationToken cancellationToken)
    {
        var links = await _linkService.GetAll();
        return _mapper.Map<IEnumerable<LinkResponse>>(links);
    }
}