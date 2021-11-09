using LinkManagerApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace LinkManagerApi.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        
        }


        public DbSet<Link> Links { get; set; }
        
        
    }
}