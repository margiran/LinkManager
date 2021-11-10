using System;
using MediatR;

namespace LinkManagerApi.Commands;

public class DeleteLinkCommand : IRequest<bool>
{
    public Guid Id { get;  }
    public DeleteLinkCommand(Guid id)
    {
        Id=id;
    }
    

}