using Authentification.Application.Interfaces;
using Authentification.Data.Context;
using Authentification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Authentification.Data.Repository
{
    public class CRUDOperations : IRepositories
    {
        private readonly AuthentificationContext _context;

        public CRUDOperations(AuthentificationContext context)
        {
            _context = context;
        }
        public async Task AddEntityToDbAsync<TEntity>(TEntity entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(e => e.UserEmail == email);
        }

        public async Task<RefreshToken?> GetTokenByUserIdAsync(Guid userId)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task UpdateEntityToDbAsync<TObject>(TObject objects)
        {
            _context.Update(objects);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTokenAsync<TEntity>(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
