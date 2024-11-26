using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Service;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DiarioDeClasse.Test
{
    public class AlunoServiceTests
    {
        private readonly AlunoService _alunoService;
        private readonly Mock<IAlunoRepository> _mockRepository;

        public AlunoServiceTests()
        {
            _mockRepository = new Mock<IAlunoRepository>();
            _alunoService = new AlunoService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllAlunos()
        {
            var alunos = new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "Aluno 1", Matricula = "123" },
                new Aluno { Id = 2, Nome = "Aluno 2", Matricula = "456" }
            };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(alunos);

            var result = await _alunoService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Aluno 1", result.First().Nome);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAluno_WhenAlunoExists()
        {
            var aluno = new Aluno { Id = 1, Nome = "Aluno 1", Matricula = "123" };
            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(aluno);

            var result = await _alunoService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(aluno.Nome, result.Nome);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenAlunoDoesNotExist()
        {
            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((Aluno)null);

            var result = await _alunoService.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryAddAndSaveMethods()
        {
            var aluno = new Aluno { Id = 1, Nome = "Aluno 1", Matricula = "123" };

            await _alunoService.AddAsync(aluno);

            _mockRepository.Verify(repo => repo.AddAlunoAsync(aluno), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAluno_WhenAlunoExists()
        {
            var existingAluno = new Aluno { Id = 1, Nome = "Aluno 1", Matricula = "123" };
            var updatedAluno = new Aluno { Nome = "Aluno Atualizado", Matricula = "789" };

            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(existingAluno);

            await _alunoService.UpdateAsync(1, updatedAluno);

            Assert.Equal("Aluno Atualizado", existingAluno.Nome);
            Assert.Equal("789", existingAluno.Matricula);
            _mockRepository.Verify(repo => repo.Update(existingAluno), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenAlunoDoesNotExist()
        {
            var updatedAluno = new Aluno { Nome = "Aluno Atualizado", Matricula = "789" };

            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((Aluno)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _alunoService.UpdateAsync(1, updatedAluno));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteAndSaveMethods_WhenAlunoExists()
        {
            var aluno = new Aluno { Id = 1, Nome = "Aluno 1", Matricula = "123" };

            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(aluno);

            await _alunoService.DeleteAsync(1);

            _mockRepository.Verify(repo => repo.DeleteAlunoAsync(1), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenAlunoDoesNotExist()
        {
            _mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((Aluno)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _alunoService.DeleteAsync(1));
        }
    }
}
