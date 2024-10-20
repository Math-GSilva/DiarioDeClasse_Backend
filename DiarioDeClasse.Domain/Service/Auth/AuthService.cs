﻿using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiarioDeClasse.Domain.Service.Auth
{
    public class AuthService
    {
        private readonly IUserRepository _usuarioRepository;
        private readonly string _jwtKey;

        public AuthService(IUserRepository usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _jwtKey = config["JwtConfig:Key"];
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(username);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(password, usuario.Password))
            {
                return null;
            }

            return usuario;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "seuIssuer",
                audience: "seuAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
