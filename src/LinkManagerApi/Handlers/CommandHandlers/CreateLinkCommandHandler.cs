using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Commands;
using LinkManagerApi.Domain;
using LinkManagerApi.Dtos;
using LinkManagerApi.Services;
using MediatR;

namespace LinkManagerApi.Handlers.CommandHandlers;

public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, LinkResponseDto>
{
    private readonly IMapper _mapper;
    private readonly ILinkService _linkService;

    public CreateLinkCommandHandler(IMapper mapper, ILinkService linkService)
    {
        _mapper = mapper;
        _linkService = linkService;
    }
    public async Task<LinkResponseDto> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var link = _mapper.Map<Link>(request);
        link.Id=Guid.NewGuid();
        await _linkService.Create(link);
        await _linkService.SaveChanges();
        return _mapper.Map<LinkResponseDto>(link);

    }
}
