using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkManagerApi.Domain;

namespace LinkManagerApi.Services
{
    public interface ILinkService
    {
       Task< List<Link>> GetAll();

        Task<Link> GetById(Guid Id);

        Task Create(Link link);

        Task<bool> Update(Link link);
        Task<bool> Delete(Guid Id);

    }
}