using Authentification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentification.Data.Context
{
    public class AuthentificationContext : DbContext
    {
        public AuthentificationContext(DbContextOptions<AuthentificationContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthentificationContext).Assembly);
        }
    }
}
