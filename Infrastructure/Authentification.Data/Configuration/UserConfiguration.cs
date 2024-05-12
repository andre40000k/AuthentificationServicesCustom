using Authentification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentification.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.UserId);
            builder.Property(p => p.UserId).ValueGeneratedOnAdd();
            builder.Property(p => p.UserFirstName).IsRequired();
            builder.Property(p => p.UserSecondName);
            builder.Property(p => p.UserEmail).IsRequired();
            builder.Property(p => p.UserPassword).IsRequired();
        }
    }
}
