namespace LinkManagerApi.Handlers.QueryHandlers;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Contracts.V1.Responses;
using LinkManagerApi.Queries;
using LinkManagerApi.Services;
using MediatR;


public class GetLinkByIdQueryHandler : IRequestHandler<GetLinkByIdQuery, LinkResponse>
{
    private readonly IMapper _mapper;
    private readonly ILinkService _linkService;

    public GetLinkByIdQueryHandler(IMapper mapper, ILinkService linkService)
    {
        _mapper = mapper;
        _linkService = linkService;
    }
    public async Task<LinkResponse> Handle(GetLinkByIdQuery request, CancellationToken cancellationToken)
    {
        var link = await _linkService.GetById(request.Id);
        if (link == null)
            return null;

        return _mapper.Map<LinkResponse>(link);
    }
}
