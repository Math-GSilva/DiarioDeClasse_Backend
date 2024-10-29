﻿using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Repository
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(DiarioDeClasseContext db)
            : base(db)
        {
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var results = await GetAll();
            return results.FirstOrDefault(usuario => usuario.Email.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<User?> AddAsync(User usuario)
        {
            var user = await Add(usuario);
            return user;
        }
    }
}
