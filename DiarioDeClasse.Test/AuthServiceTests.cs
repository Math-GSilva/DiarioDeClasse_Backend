using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Test
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IConfiguration> _mockConfig;

        public AuthServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(config => config["Jwt:Key"]).Returns("alskjdlkajsdlkajslkdjaldjioqwjda~çlsdkas123123123");

            _authService = new AuthService(_mockUserRepository.Object, _mockConfig.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnUser_WhenCredentialsAreValid()
        {
            var username = "testuser";
            var password = "testpassword";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User { Email = "testuser@example.com", Password = hashedPassword };
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(username))
                .ReturnsAsync(user);

            var result = await _authService.AuthenticateAsync(username, password);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var username = "nonexistentuser";
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(username))
                .ReturnsAsync((User)null);

            var result = await _authService.AuthenticateAsync(username, "password");

            Assert.Null(result);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnNull_WhenPasswordIsInvalid()
        {
            var username = "testuser";
            var password = "wrongpassword";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("correctpassword");

            var user = new User { Email = "testuser@example.com", Password = hashedPassword };
            _mockUserRepository.Setup(repo => repo.GetByEmailAsync(username))
                .ReturnsAsync(user);

            var result = await _authService.AuthenticateAsync(username, password);

            Assert.Null(result);
        }

        [Fact]
        public void GenerateJwtToken_ShouldReturnValidToken()
        {
            var user = new User { Email = "testuser@example.com" };

            var token = _authService.GenerateJwtToken(user);

            Assert.NotNull(token);
            Assert.IsType<string>(token);
        }

        [Fact]
        public void ValidateToken_ShouldReturnTrue_WhenTokenIsValid()
        {
            var user = new User { Email = "testuser@example.com" };
            var token = _authService.GenerateJwtToken(user);

            var result = _authService.ValidateToken(token);

            Assert.True(result);
        }

        [Fact]
        public void ValidateToken_ShouldReturnFalse_WhenTokenIsInvalid()
        {
            var invalidToken = "invalid_token";

            var result = _authService.ValidateToken(invalidToken);

            Assert.False(result);
        }
    }
}
