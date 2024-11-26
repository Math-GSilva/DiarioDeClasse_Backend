using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DiarioDeClasse.Test
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfUsers()
        {
            var users = new List<User>
            {
                new User { Id = 1, Nome = "User A", Email = "usera@example.com", DataNascimento = new DateTime(2000, 1, 1), Password = "password", Tipo = "Professor" },
                new User { Id = 2, Nome = "User B", Email = "userb@example.com", DataNascimento = new DateTime(2001, 2, 2), Password = "password", Tipo = "Aluno" }
            };

            _mockUserRepository.Setup(repo => repo.GetAll()).ReturnsAsync(users);

            var result = await _userService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var user = new User { Id = 1, Nome = "User A", Email = "usera@example.com", DataNascimento = new DateTime(2000, 1, 1), Password = "password", Tipo = "Professor" };

            _mockUserRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(user);

            var result = await _userService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Nome, result.Nome);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            _mockUserRepository.Setup(repo => repo.GetById(999)).ReturnsAsync((User)null);

            var result = await _userService.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnCreatedUser_WhenUserIsValid()
        {
            var password = "password";
            var user = new User { Nome = "User C", Email = "userc@example.com", DataNascimento = new DateTime(2002, 3, 3), Password = password, Tipo = "Aluno" };

            _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            var result = await _userService.AddAsync(user);

            Assert.NotNull(result);
            Assert.Equal(user.Nome, result.Nome);
            Assert.Equal(user.Email, result.Email);
            Assert.True(BCrypt.Net.BCrypt.Verify(password, result.Password));
        }


        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser_WhenUserExists()
        {
            var existingUser = new User { Id = 1, Nome = "User A", Email = "usera@example.com", DataNascimento = new DateTime(2000, 1, 1), Password = "password", Tipo = "Professor" };
            var updatedUser = new User { Id = 1, Nome = "Updated User A", Email = "updatedusera@example.com", DataNascimento = new DateTime(2001, 1, 1), Password = "newpassword", Tipo = "Aluno" };

            _mockUserRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(existingUser);
            _mockUserRepository.Setup(repo => repo.Update(existingUser)).Returns(Task.CompletedTask);

            await _userService.UpdateAsync(1, updatedUser);

            _mockUserRepository.Verify(repo => repo.Update(existingUser), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowKeyNotFoundException_WhenUserDoesNotExist()
        {
            var user = new User { Id = 999, Nome = "Nonexistent User", Email = "nonexistent@example.com", DataNascimento = new DateTime(2002, 4, 4), Password = "password", Tipo = "Aluno" };

            _mockUserRepository.Setup(repo => repo.GetById(999)).ReturnsAsync((User)null);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.UpdateAsync(999, user));

            Assert.Equal("User not found.", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteUser_WhenUserExists()
        {
            var user = new User { Id = 1, Nome = "User A", Email = "usera@example.com", DataNascimento = new DateTime(2000, 1, 1), Password = "password", Tipo = "Professor" };

            _mockUserRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.DeleteUserAsync(1)).Returns(Task.CompletedTask);

            await _userService.DeleteAsync(1);

            _mockUserRepository.Verify(repo => repo.DeleteUserAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenUserDoesNotExist()
        {
            _mockUserRepository.Setup(repo => repo.GetById(999)).ReturnsAsync((User)null);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.DeleteAsync(999));

            Assert.Equal("User not found.", exception.Message);
        }

        [Fact]
        public async Task GetAllProfessoresAsync_ShouldReturnListOfProfessores()
        {
            var professores = new List<User>
            {
                new User { Id = 1, Nome = "Professor A", Email = "professora@example.com", DataNascimento = new DateTime(1980, 1, 1), Password = "password", Tipo = "Professor" },
                new User { Id = 2, Nome = "Professor B", Email = "professorb@example.com", DataNascimento = new DateTime(1985, 2, 2), Password = "password", Tipo = "Professor" }
            };

            _mockUserRepository.Setup(repo => repo.GetAllProfessoresAsync()).ReturnsAsync(professores);

            var result = await _userService.GetAllProfessoresAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
