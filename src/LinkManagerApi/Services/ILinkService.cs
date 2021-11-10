using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkManagerApi.Domain;

namespace LinkManagerApi.Services
{
    public interface ILinkService
    {
        Task<IEnumerable<Link>> GetAll(string updateAt);
        Task<IEnumerable<Link>> GetUpdatedAfter(DateTimeOffset updateAt);

        Task<Link> GetById(Guid id);
        Task Create(Link link);
        Task<bool> LinkExist(Guid id);
        Task<bool> Update(Link link);
        Task<bool> Delete(Guid id);

        Task<bool> SaveChanges();

    }
}