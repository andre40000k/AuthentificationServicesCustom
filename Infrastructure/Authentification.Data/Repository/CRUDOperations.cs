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
    }
}
