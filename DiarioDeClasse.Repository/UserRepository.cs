using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiarioDeClasse.Repository
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(DiarioDeClasseContext db)
            : base(db)
        {
        }

        public async Task<User?> GetByIdWithIncludesAsync(int id, IEnumerable<string> includes)
        {
            return await Get(id, includes);
        }

        public async Task<User?> GetById(int id)
        {
            return await Get(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var results = await GetAll();
            return results.FirstOrDefault(user => user.Email != null &&
                                                  user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<User> AddUserAsync(User user)
        {
            return await Add(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await Update(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await Delete(id);
        }

        public async Task<IEnumerable<User>> GetAllProfessoresAsync()
        {
            return await _db.Set<User>().Where(u => u.Tipo == "professor").ToListAsync();
        }
    }
}
