using System.Data.Entity;
using QuickElastic.Core.Domain;

namespace QuickElastic.Data
{
    public class QuickElasticContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
