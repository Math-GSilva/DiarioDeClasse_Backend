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
    public class AulaServiceTests
    {
        private readonly AulaService _aulaService;
        private readonly Mock<IAulaRepository> _mockRepository;

        public AulaServiceTests()
        {
            _mockRepository = new Mock<IAulaRepository>();
            _aulaService = new AulaService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByTurmaIdAsync_ShouldReturnAulas_WhenTurmaIdExists()
        {
            var turmaId = 1;
            var aulas = new List<Aula>
            {
                new Aula { Id = 1, TurmaId = turmaId, Data = DateTime.Now },
                new Aula { Id = 2, TurmaId = turmaId, Data = DateTime.Now.AddDays(1) }
            };

            _mockRepository.Setup(repo => repo.GetByTurmaIdAsync(turmaId)).ReturnsAsync(aulas);

            var result = await _aulaService.GetByTurmaIdAsync(turmaId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByDateAsync_ShouldReturnAulas_WhenDateMatches()
        {
            var date = DateTime.Now.Date;
            var aulas = new List<Aula>
            {
                new Aula { Id = 1, TurmaId = 1, Data = date },
                new Aula { Id = 2, TurmaId = 2, Data = date }
            };

            _mockRepository.Setup(repo => repo.GetByDateAsync(date)).ReturnsAsync(aulas);

            var result = await _aulaService.GetByDateAsync(date);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedAula()
        {
            var aula = new Aula { Id = 1, TurmaId = 1, Data = DateTime.Now };
            _mockRepository.Setup(repo => repo.AddAulaAsync(aula)).ReturnsAsync(aula);

            var result = await _aulaService.AddAsync(aula);

            Assert.NotNull(result);
            Assert.Equal(aula.Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateAula_WhenAulaExists()
        {
            var id = 1;
            var aula = new Aula { Id = id, TurmaId = 1, Data = DateTime.Now };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync(aula);

            await _aulaService.UpdateAsync(id, aula);

            _mockRepository.Verify(repo => repo.UpdateAulaAsync(aula), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenAulaDoesNotExist()
        {
            var id = 1;
            var aula = new Aula { Id = id, TurmaId = 1, Data = DateTime.Now };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync((Aula)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _aulaService.UpdateAsync(id, aula));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteMethod_WhenAulaExists()
        {
            var id = 1;
            var aula = new Aula { Id = id, TurmaId = 1, Data = DateTime.Now };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync(aula);

            await _aulaService.DeleteAsync(id);

            _mockRepository.Verify(repo => repo.DeleteAulaAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenAulaDoesNotExist()
        {
            var id = 1;

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync((Aula)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _aulaService.DeleteAsync(id));
        }
    }
}
