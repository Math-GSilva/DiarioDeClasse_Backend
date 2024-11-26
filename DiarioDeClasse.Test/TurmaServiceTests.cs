using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Service;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DiarioDeClasse.Test
{
    public class TurmaServiceTests
    {
        private readonly TurmaService _turmaService;
        private readonly Mock<ITurmaRepository> _mockTurmaRepository;

        public TurmaServiceTests()
        {
            _mockTurmaRepository = new Mock<ITurmaRepository>();
            _turmaService = new TurmaService(_mockTurmaRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfTurmas()
        {
            var turmas = new List<Turma>
            {
                new Turma { Id = 1, Nome = "Turma A", AnoLetivo = 2024, ProfessorId = 101 },
                new Turma { Id = 2, Nome = "Turma B", AnoLetivo = 2024, ProfessorId = 102 }
            };

            _mockTurmaRepository.Setup(repo => repo.GetAll()).ReturnsAsync(turmas);

            var result = await _turmaService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTurma_WhenTurmaExists()
        {
            var turma = new Turma { Id = 1, Nome = "Turma A", AnoLetivo = 2024, ProfessorId = 101 };

            _mockTurmaRepository.Setup(repo => repo.Get(1)).ReturnsAsync(turma);

            var result = await _turmaService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(turma.Id, result.Id);
            Assert.Equal(turma.Nome, result.Nome);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenTurmaDoesNotExist()
        {
            _mockTurmaRepository.Setup(repo => repo.Get(999)).ReturnsAsync((Turma)null);

            var result = await _turmaService.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnCreatedTurma_WhenTurmaIsValid()
        {
            var turma = new Turma { Nome = "Turma C", AnoLetivo = 2024, ProfessorId = 103 };

            _mockTurmaRepository.Setup(repo => repo.AddTurmaAsync(turma)).ReturnsAsync(turma);

            var result = await _turmaService.AddAsync(turma);

            Assert.NotNull(result);
            Assert.Equal(turma.Nome, result.Nome);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTurma_WhenTurmaExists()
        {
            var existingTurma = new Turma { Id = 1, Nome = "Turma A", AnoLetivo = 2024, ProfessorId = 101 };
            var updatedTurma = new Turma { Id = 1, Nome = "Turma A Atualizada", AnoLetivo = 2024, ProfessorId = 102 };

            _mockTurmaRepository.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(existingTurma);
            _mockTurmaRepository.Setup(repo => repo.UpdateTurmaAsync(existingTurma)).Returns(Task.CompletedTask);

            await _turmaService.UpdateAsync(1, existingTurma);

            _mockTurmaRepository.Verify(repo => repo.UpdateTurmaAsync(existingTurma), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowKeyNotFoundException_WhenTurmaDoesNotExist()
        {
            var turma = new Turma { Id = 999, Nome = "Turma Inexistente", AnoLetivo = 2024, ProfessorId = 103 };

            _mockTurmaRepository.Setup(repo => repo.Get(999)).ReturnsAsync((Turma)null);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _turmaService.UpdateAsync(999, turma));

            Assert.Equal("Turma not found.", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteTurma_WhenTurmaExists()
        {
            var turma = new Turma { Id = 1, Nome = "Turma A", AnoLetivo = 2024, ProfessorId = 101 };

            _mockTurmaRepository.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(turma);
            _mockTurmaRepository.Setup(repo => repo.DeleteTurmaAsync(turma.Id)).Returns(Task.CompletedTask);

            await _turmaService.DeleteAsync(1);

            _mockTurmaRepository.Verify(repo => repo.DeleteTurmaAsync(turma.Id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenTurmaDoesNotExist()
        {
            _mockTurmaRepository.Setup(repo => repo.Get(999)).ReturnsAsync((Turma)null);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _turmaService.DeleteAsync(999));

            Assert.Equal("Turma not found.", exception.Message);
        }
    }
}
