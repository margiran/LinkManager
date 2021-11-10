using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Commands;
using LinkManagerApi.Domain;
using LinkManagerApi.Dtos;
using LinkManagerApi.Services;
using MediatR;

namespace LinkManagerApi.Handlers.CommandHandlers;

public class UpdateLinkCommandHandler : IRequestHandler<UpdateLinkCommand, LinkResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ILinkService _linkService;

    public UpdateLinkCommandHandler(IMapper mapper, ILinkService linkService)
    {
        _mapper=mapper;
        _linkService=linkService;
    }
    public async Task<LinkResponseDto> Handle(UpdateLinkCommand request, CancellationToken cancellationToken)
    {
            var link= _mapper.Map<Link> (request);
            var update=await _linkService.Update(link);
            if (!update)
                return null;
            await _linkService.SaveChanges();
            return _mapper.Map<LinkResponseDto>(link);
    }
}
