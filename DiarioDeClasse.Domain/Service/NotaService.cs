using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Interface.Services;

namespace DiarioDeClasse.Domain.Service
{
    public class NotaService : INotaService
    {
        private readonly INotaRepository _repository;

        public NotaService(INotaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Nota>> GetByAlunoIdAsync(int alunoId)
        {
            return await _repository.GetByAlunoIdAsync(alunoId);
        }

        public async Task<IEnumerable<Nota>> GetByTurmaIdAsync(int turmaId)
        {
            return await _repository.GetByTurmaIdAsync(turmaId);
        }

        public async Task<IEnumerable<Nota>> GetByAvaliacaoAsync(string avaliacao)
        {
            return await _repository.GetByAvaliacaoAsync(avaliacao);
        }

        public async Task<Nota> AddAsync(Nota nota)
        {
            return await _repository.AddNotaAsync(nota);
        }

        public async Task UpdateAsync(int id, Nota nota)
        {
            if (await _repository.Get(id) == null)
                throw new KeyNotFoundException("Nota não encontrada.");

            await _repository.UpdateNotaAsync(nota);
        }

        public async Task DeleteAsync(int id)
        {
            if (await _repository.Get(id) == null)
                throw new KeyNotFoundException("Nota não encontrada.");

            await _repository.DeleteNotaAsync(id);
        }
    }
}
