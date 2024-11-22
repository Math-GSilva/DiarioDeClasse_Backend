using DiarioDeClasse.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Repository
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
        Task<User?> GetByIdWithIncludesAsync(int id, IEnumerable<string> includes);
        Task<IEnumerable<User>> GetAllProfessoresAsync();
        Task<User?> GetById(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
