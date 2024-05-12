using Authentification.Domain.Entities;

namespace Authentification.Application.Interfaces
{
    public interface IRepositories
    {
        Task AddEntityToDbAsync<TObject>(TObject objects);

        Task<User?> GetUserByEmailAsync(string email);
    }
}
