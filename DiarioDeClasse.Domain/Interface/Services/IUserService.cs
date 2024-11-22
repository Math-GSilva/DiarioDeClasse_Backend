using DiarioDeClasse.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetAllProfessoresAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task UpdateAsync(int id, User user);
        Task DeleteAsync(int id);
    }
}
