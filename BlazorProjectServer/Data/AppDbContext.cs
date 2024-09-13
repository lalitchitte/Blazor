using Microsoft.EntityFrameworkCore;

namespace BlazorProjectServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
