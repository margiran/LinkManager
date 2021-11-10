using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkManagerApi.Data;
using LinkManagerApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace LinkManagerApi.Services
{
    public class SqlServerLinkService : ILinkService
    {

        private readonly ApplicationDbContext _dbContext;

        public SqlServerLinkService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(Link link)
        {
            await _dbContext.Links.AddAsync(link);
        }

        public async Task<bool> Delete(Guid id)
        {
            var link = await _dbContext.Links.FindAsync(id);
            if (link == null)
                return false;

            link.IsDelete = true;
            return await Update(link);
        }

        public async Task<IEnumerable<Link>> GetAll()
        {
            return  await _dbContext.Links.ToListAsync();
        }

        public async Task<Link> GetById(Guid id)
        {
            return await _dbContext.Links.FindAsync(id);
        }

        public async Task<bool> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Link link)
        {
            // var exist=await GetById(link.Id) != null;
            // if(!exist)
            //     return false;
            link.UpDateAt=DateTimeOffset.UtcNow;
            _dbContext.Links.Update(link);
            return true;
        }
    }
}