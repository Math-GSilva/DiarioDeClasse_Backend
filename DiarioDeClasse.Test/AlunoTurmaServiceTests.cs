using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Service;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DiarioDeClasse.Test
{
    public class AlunoTurmaServiceTests
    {
        private readonly AlunoTurmaService _alunoTurmaService;
        private readonly Mock<IAlunoTurmaRepository> _mockRepository;

        public AlunoTurmaServiceTests()
        {
            _mockRepository = new Mock<IAlunoTurmaRepository>();
            _alunoTurmaService = new AlunoTurmaService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByAlunoIdAsync_ShouldReturnAlunoTurmas_WhenAlunoIdExists()
        {
            var alunoId = 1;
            var alunoTurmas = new List<AlunoTurma>
            {
                new AlunoTurma { Id = 1, AlunoId = alunoId, TurmaId = 1 },
                new AlunoTurma { Id = 2, AlunoId = alunoId, TurmaId = 2 }
            };
            _mockRepository.Setup(repo => repo.GetByAlunoIdAsync(alunoId)).ReturnsAsync(alunoTurmas);

            var result = await _alunoTurmaService.GetByAlunoIdAsync(alunoId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByTurmaIdAsync_ShouldReturnAlunoTurmas_WhenTurmaIdExists()
        {
            var turmaId = 1;
            var alunoTurmas = new List<AlunoTurma>
            {
                new AlunoTurma { Id = 1, AlunoId = 1, TurmaId = turmaId },
                new AlunoTurma { Id = 2, AlunoId = 2, TurmaId = turmaId }
            };
            _mockRepository.Setup(repo => repo.GetByTurmaIdAsync(turmaId)).ReturnsAsync(alunoTurmas);

            var result = await _alunoTurmaService.GetByTurmaIdAsync(turmaId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AlunoJaMatriculadoAsync_ShouldReturnTrue_WhenAlunoIsAlreadyEnrolled()
        {
            var alunoId = 1;
            var turmaId = 1;
            _mockRepository.Setup(repo => repo.AlunoJaMatriculadoAsync(alunoId, turmaId)).ReturnsAsync(true);

            var result = await _alunoTurmaService.AlunoJaMatriculadoAsync(alunoId, turmaId);

            Assert.True(result);
        }

        [Fact]
        public async Task AlunoJaMatriculadoAsync_ShouldReturnFalse_WhenAlunoIsNotEnrolled()
        {
            var alunoId = 1;
            var turmaId = 1;
            _mockRepository.Setup(repo => repo.AlunoJaMatriculadoAsync(alunoId, turmaId)).ReturnsAsync(false);

            var result = await _alunoTurmaService.AlunoJaMatriculadoAsync(alunoId, turmaId);

            Assert.False(result);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryAddMethod()
        {
            var alunoTurma = new AlunoTurma { Id = 1, AlunoId = 1, TurmaId = 1 };

            await _alunoTurmaService.AddAsync(alunoTurma);

            _mockRepository.Verify(repo => repo.AddAlunoTurmaAsync(alunoTurma), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAlunoTurma_WhenAlunoTurmaExists()
        {
            var id = 1;
            var alunoTurma = new AlunoTurma { Id = id, AlunoId = 1, TurmaId = 2 };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync(alunoTurma);

            await _alunoTurmaService.UpdateAsync(id, alunoTurma);

            _mockRepository.Verify(repo => repo.UpdateAlunoTurmaAsync(alunoTurma), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenAlunoTurmaDoesNotExist()
        {
            var id = 1;
            var alunoTurma = new AlunoTurma { Id = id, AlunoId = 1, TurmaId = 2 };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync((AlunoTurma)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _alunoTurmaService.UpdateAsync(id, alunoTurma));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteMethod_WhenAlunoTurmaExists()
        {
            var id = 1;
            var alunoTurma = new AlunoTurma { Id = id, AlunoId = 1, TurmaId = 1 };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync(alunoTurma);

            await _alunoTurmaService.DeleteAsync(id);

            _mockRepository.Verify(repo => repo.DeleteAlunoTurmaAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenAlunoTurmaDoesNotExist()
        {
            var id = 1;

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync((AlunoTurma)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _alunoTurmaService.DeleteAsync(id));
        }
    }
}
