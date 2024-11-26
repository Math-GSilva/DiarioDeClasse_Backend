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
    public class NotaServiceTests
    {
        private readonly NotaService _notaService;
        private readonly Mock<INotaRepository> _mockRepository;

        public NotaServiceTests()
        {
            _mockRepository = new Mock<INotaRepository>();
            _notaService = new NotaService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByAlunoIdAsync_ShouldReturnNotas_WhenAlunoIdExists()
        {
            var alunoId = 1;
            var notas = new List<Nota>
            {
                new Nota { Id = 1, AlunoId = alunoId, TurmaId = 1, Avaliacao = "Nota 1", ValorNota = 7.5m },
                new Nota { Id = 2, AlunoId = alunoId, TurmaId = 1, Avaliacao = "Nota 2", ValorNota = 8.0m }
            };

            _mockRepository.Setup(repo => repo.GetByAlunoIdAsync(alunoId)).ReturnsAsync(notas);

            var result = await _notaService.GetByAlunoIdAsync(alunoId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, nota => Assert.Equal(alunoId, nota.AlunoId));
        }

        [Fact]
        public async Task GetByTurmaIdAsync_ShouldReturnNotas_WhenTurmaIdExists()
        {
            var turmaId = 1;
            var notas = new List<Nota>
            {
                new Nota { Id = 1, AlunoId = 1, TurmaId = turmaId, Avaliacao = "Nota 1", ValorNota = 7.5m },
                new Nota { Id = 2, AlunoId = 2, TurmaId = turmaId, Avaliacao = "Nota 2", ValorNota = 8.0m }
            };

            _mockRepository.Setup(repo => repo.GetByTurmaIdAsync(turmaId)).ReturnsAsync(notas);

            var result = await _notaService.GetByTurmaIdAsync(turmaId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, nota => Assert.Equal(turmaId, nota.TurmaId));
        }

        [Fact]
        public async Task GetByAvaliacaoAsync_ShouldReturnNotas_WhenAvaliacaoExists()
        {
            var avaliacao = "Nota 1";
            var notas = new List<Nota>
            {
                new Nota { Id = 1, AlunoId = 1, TurmaId = 1, Avaliacao = avaliacao, ValorNota = 7.5m },
                new Nota { Id = 2, AlunoId = 2, TurmaId = 1, Avaliacao = avaliacao, ValorNota = 8.0m }
            };

            _mockRepository.Setup(repo => repo.GetByAvaliacaoAsync(avaliacao)).ReturnsAsync(notas);

            var result = await _notaService.GetByAvaliacaoAsync(avaliacao);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, nota => Assert.Equal(avaliacao, nota.Avaliacao));
        }

        [Fact]
        public async Task AddAsync_ShouldReturnAddedNota()
        {
            var nota = new Nota
            {
                AlunoId = 1,
                TurmaId = 1,
                Avaliacao = "Nota 1",
                ValorNota = 7.5m
            };

            _mockRepository.Setup(repo => repo.AddNotaAsync(nota)).ReturnsAsync(nota);

            var result = await _notaService.AddAsync(nota);

            Assert.NotNull(result);
            Assert.Equal(nota.Avaliacao, result.Avaliacao);
            Assert.Equal(nota.AlunoId, result.AlunoId);
            Assert.Equal(nota.TurmaId, result.TurmaId);
            Assert.Equal(nota.ValorNota, result.ValorNota);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateNota_WhenNotaExists()
        {
            var id = 1;
            var nota = new Nota
            {
                Id = id,
                AlunoId = 1,
                TurmaId = 1,
                Avaliacao = "Nota 1",
                ValorNota = 7.5m
            };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync(nota);
            _mockRepository.Setup(repo => repo.UpdateNotaAsync(It.IsAny<Nota>())).Returns(Task.CompletedTask);

            await _notaService.UpdateAsync(id, nota);

            _mockRepository.Verify(repo => repo.UpdateNotaAsync(It.Is<Nota>(n => n.Id == id)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenNotaDoesNotExist()
        {
            var id = 1;
            var nota = new Nota
            {
                Id = id,
                AlunoId = 1,
                TurmaId = 1,
                Avaliacao = "Nota 1",
                ValorNota = 7.5m
            };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync((Nota)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _notaService.UpdateAsync(id, nota));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteMethod_WhenNotaExists()
        {
            var id = 1;
            var nota = new Nota { Id = id, AlunoId = 1, TurmaId = 1, Avaliacao = "Nota 1", ValorNota = 7.5m };

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync(nota);
            _mockRepository.Setup(repo => repo.DeleteNotaAsync(id)).Returns(Task.CompletedTask);

            await _notaService.DeleteAsync(id);

            _mockRepository.Verify(repo => repo.DeleteNotaAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenNotaDoesNotExist()
        {
            var id = 1;

            _mockRepository.Setup(repo => repo.Get(id)).ReturnsAsync((Nota)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _notaService.DeleteAsync(id));
        }
    }
}
