using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkManagerApi.Domain;

namespace LinkManagerApi.Services
{
    public interface ILinkService
    {
       Task< IEnumerable<Link>> GetAll();

        Task<Link> GetById(Guid id);

        Task Create(Link link);

        Task<bool> Update(Link link);
        Task<bool> Delete(Guid id);

        Task<bool> SaveChanges();

    }
}