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
    public class ChamadaServiceTests
    {
        private readonly ChamadaService _chamadaService;
        private readonly Mock<IChamadaRepository> _mockRepository;

        public ChamadaServiceTests()
        {
            _mockRepository = new Mock<IChamadaRepository>();
            _chamadaService = new ChamadaService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByAulaIdAsync_ShouldReturnChamadas_WhenAulaIdExists()
        {
            var aulaId = 1;
            var chamadas = new List<Chamada>
            {
                new Chamada { Id = 1, AulaId = aulaId, AlunoId = 1, StatusPresenca = "Presente" },
                new Chamada { Id = 2, AulaId = aulaId, AlunoId = 2, StatusPresenca = "Ausente" }
            };

            _mockRepository.Setup(repo => repo.GetByAulaIdAsync(aulaId)).ReturnsAsync(chamadas);

            var result = await _chamadaService.GetByAulaIdAsync(aulaId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, chamada => Assert.Equal(aulaId, chamada.AulaId));
        }

        [Fact]
        public async Task GetByAlunoIdAsync_ShouldReturnChamadas_WhenAlunoIdExists()
        {
            var alunoId = 1;
            var chamadas = new List<Chamada>
            {
                new Chamada { Id = 1, AulaId = 1, AlunoId = alunoId, StatusPresenca = "Presente" },
                new Chamada { Id = 2, AulaId = 2, AlunoId = alunoId, StatusPresenca = "Ausente" }
            };

            _mockRepository.Setup(repo => repo.GetByAlunoIdAsync(alunoId)).ReturnsAsync(chamadas);

            var result = await _chamadaService.GetByAlunoIdAsync(alunoId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, chamada => Assert.Equal(alunoId, chamada.AlunoId));
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedChamada()
        {
            var chamada = new Chamada
            {
                AulaId = 1,
                AlunoId = 1,
                StatusPresenca = "Presente",
                Observacao = "Tudo certo"
            };
            _mockRepository.Setup(repo => repo.AddChamadaAsync(chamada)).ReturnsAsync(chamada);

            var result = await _chamadaService.AddAsync(chamada);

            Assert.NotNull(result);
            Assert.Equal(chamada.StatusPresenca, result.StatusPresenca);
            Assert.Equal(chamada.AulaId, result.AulaId);
            Assert.Equal(chamada.AlunoId, result.AlunoId);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateChamada_WhenChamadaExists()
        {
            var id = 1;
            var chamada = new Chamada
            {
                Id = id,
                AulaId = 1,
                AlunoId = 1,
                StatusPresenca = "Presente",
                Observacao = "Tudo certo"
            };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync(chamada);
            _mockRepository.Setup(repo => repo.UpdateChamadaAsync(It.IsAny<Chamada>())).Returns(Task.CompletedTask);

            await _chamadaService.UpdateAsync(id, chamada);

            _mockRepository.Verify(repo => repo.UpdateChamadaAsync(It.Is<Chamada>(c => c.Id == id)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenChamadaDoesNotExist()
        {
            var id = 1;
            var chamada = new Chamada
            {
                Id = id,
                AulaId = 1,
                AlunoId = 1,
                StatusPresenca = "Presente",
                Observacao = "Tudo certo"
            };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync((Chamada)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _chamadaService.UpdateAsync(id, chamada));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteMethod_WhenChamadaExists()
        {
            var id = 1;
            var chamada = new Chamada { Id = id, AulaId = 1, AlunoId = 1, StatusPresenca = "Presente" };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync(chamada);
            _mockRepository.Setup(repo => repo.DeleteChamadaAsync(id)).Returns(Task.CompletedTask);

            await _chamadaService.DeleteAsync(id);

            _mockRepository.Verify(repo => repo.DeleteChamadaAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenChamadaDoesNotExist()
        {
            var id = 1;

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync((Chamada)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _chamadaService.DeleteAsync(id));
        }
    }
}
