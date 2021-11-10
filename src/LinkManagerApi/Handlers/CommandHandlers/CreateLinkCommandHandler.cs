using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Commands;
using LinkManagerApi.Contracts.V1.Responses;
using LinkManagerApi.Domain;
using LinkManagerApi.Services;
using MediatR;

namespace LinkManagerApi.Handlers.CommandHandlers
{
    public class CreateLinkCommandHandler : IRequestHandler<CreateLinkCommand, LinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILinkService _linkService;

        public CreateLinkCommandHandler(IMapper mapper,ILinkService linkService)
        {
            _mapper=mapper;
            _linkService=linkService;
        }
        public async Task<LinkResponse> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
        {
             var link= _mapper.Map<Link>(request);
 
            await  _linkService.Create(link);
            await _linkService.SaveChanges();
            return _mapper.Map<LinkResponse>(link);

        }
    }
}