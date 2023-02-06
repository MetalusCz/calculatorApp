
using Kalkulacka.Models;
using Microsoft.EntityFrameworkCore;

namespace Kalkulacka.Data {
    public class ApplicationDbContext : DbContext {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Kalkulacka.Models.Calculator> Results { get; set; } = default;
        
    }
}
