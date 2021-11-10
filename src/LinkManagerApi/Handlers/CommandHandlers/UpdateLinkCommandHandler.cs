using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Commands;
using LinkManagerApi.Contracts.V1.Responses;
using LinkManagerApi.Domain;
using LinkManagerApi.Services;
using MediatR;

namespace LinkManagerApi.Handlers.CommandHandlers;

public class UpdateLinkCommandHandler : IRequestHandler<UpdateLinkCommand, LinkResponse>
{
    private readonly IMapper _mapper;
    private readonly ILinkService _linkService;

    public UpdateLinkCommandHandler(IMapper mapper, ILinkService linkService)
    {
        _mapper=mapper;
        _linkService=linkService;
    }
    public async Task<LinkResponse> Handle(UpdateLinkCommand request, CancellationToken cancellationToken)
    {
            var link= _mapper.Map<Link> (request);
            await _linkService.Update(link);
            var update= await _linkService.SaveChanges();
            if (!update)
                return null;
            return _mapper.Map<LinkResponse>(link);
    }
}
