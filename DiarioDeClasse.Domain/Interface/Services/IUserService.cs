using DiarioDeClasse.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface IUserService
    {
        public Task<User?> SaveUsuarioAsync(User usuario);
    }
}
