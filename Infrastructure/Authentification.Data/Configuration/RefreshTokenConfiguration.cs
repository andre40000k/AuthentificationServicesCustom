using Authentification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentification.Data.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(k => k.TokenId);
            builder.Property(p => p.TokenId).ValueGeneratedOnAdd();
            builder.Property(p => p.Token).IsRequired();
            builder.Property(p => p.RefreshTokenExpiryTime).IsRequired();

            builder.HasOne(u => u.User)
                .WithOne(t => t.RefreshToken)
                .HasForeignKey<RefreshToken>(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("RefreshToken");
        }
    }
}
