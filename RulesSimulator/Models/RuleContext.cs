using Microsoft.EntityFrameworkCore;

namespace RulesSimulator.Models
{
    public class RuleContext : DbContext
    {
        public RuleContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Rules> Rules { get; set; }
    }
}
