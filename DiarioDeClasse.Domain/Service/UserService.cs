using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Interface.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<User> AddAsync(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _repository.AddUserAsync(user);
            await _repository.SaveChangesAsync();
            return user;
        }



        public async Task UpdateAsync(int id, User user)
        {
            var existingUser = await _repository.GetById(id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found.");

            existingUser.Nome = user.Nome;
            existingUser.Email = user.Email;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Password = user.Password;
            existingUser.Tipo = user.Tipo;

            await _repository.Update(existingUser);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            await _repository.DeleteUserAsync(id);
            await _repository.SaveChangesAsync();
        }

        public Task<IEnumerable<User>> GetAllProfessoresAsync()
        {
            return _repository.GetAllProfessoresAsync();
        }
    }
}
