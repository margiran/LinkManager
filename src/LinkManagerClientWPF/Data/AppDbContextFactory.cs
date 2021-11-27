using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LinkManagerClientWPF.Data
{
    public class AppDbContextFactory
    {
        private readonly string _connectionString;

        public AppDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public AppDbContext CreateDbContext( )
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite(_connectionString).Options;
            return new AppDbContext(options);
        }
    }
}