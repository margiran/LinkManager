using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkManagerApi.Domain;

namespace LinkManagerApi.Services
{
    public class SqlServerLinkService : ILinkService
    {
        public async Task Create(Link link)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(Guid Id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Link>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Link> GetById(Guid Id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(Link link)
        {
            throw new System.NotImplementedException();
        }
    }
}