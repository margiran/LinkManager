using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Dtos;
using LinkManagerApi.Queries;
using LinkManagerApi.Services;
using MediatR;

namespace LinkManagerApi.Handlers.QueryHandlers;
public class GetAllLinksQueryHandler : IRequestHandler<GetAllLinksQuery, IEnumerable<LinkResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly ILinkService _linkService;

    public GetAllLinksQueryHandler(IMapper mapper, ILinkService linkService)
    {
        _mapper = mapper;
        _linkService = linkService;
    }
    public async Task<IEnumerable<LinkResponseDto>> Handle(GetAllLinksQuery request, CancellationToken cancellationToken)
    {

        var links = await _linkService.GetAll(request.UpdateAt,cancellationToken);
        return _mapper.Map<IEnumerable<LinkResponseDto>>(links);
    }
}