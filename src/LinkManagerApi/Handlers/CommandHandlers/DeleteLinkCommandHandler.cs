using System.Threading;
using System.Threading.Tasks;
using LinkManagerApi.Commands;
using LinkManagerApi.Services;
using MediatR;

namespace LinkManagerApi.Handlers.CommandHandlers;

public class DeleteLinkCommandHandler : IRequestHandler<DeleteLinkCommand, bool>
{
    private readonly ILinkService _linkService;

    public DeleteLinkCommandHandler(ILinkService linkService)
    {
        _linkService = linkService;
    }
    public async Task<bool> Handle(DeleteLinkCommand request, CancellationToken cancellationToken)
    {
        var delete = await _linkService.Delete(request.Id);
        if (!delete)
            return false;

        await _linkService.SaveChanges();
        return true;

    }
}
