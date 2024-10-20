using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface;
using DiarioDeClasse.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User?> SaveUsuarioAsync(User usuario)
        {
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            return await userRepository.AddAsync(usuario);
        }
    }
}
