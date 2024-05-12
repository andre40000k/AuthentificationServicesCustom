using Authentification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentification.Application.Interfaces
{
    public interface IRepositories
    {
        Task AddEntityToDbAsync<TObject>(TObject objects);

        Task UpdateEntityToDbAsync<TObject>(TObject objects);

        Task<User?> GetUserByEmailAsync(string email);

        Task<RefreshToken?> GetTokenByUserIdAsync(Guid userId);

        Task DeleteTokenAsync<TEntity>(TEntity entity);
    }
}
