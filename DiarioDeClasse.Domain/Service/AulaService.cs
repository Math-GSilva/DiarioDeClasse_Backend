using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Interface.Services;

namespace DiarioDeClasse.Domain.Service
{
    public class AulaService : IAulaService
    {
        private readonly IAulaRepository _repository;

        public AulaService(IAulaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Aula>> GetByTurmaIdAsync(int turmaId)
        {
            return await _repository.GetByTurmaIdAsync(turmaId);
        }

        public async Task<IEnumerable<Aula>> GetByDateAsync(DateTime date)
        {
            return await _repository.GetByDateAsync(date);
        }

        public async Task<Aula> AddAsync(Aula aula)
        {
            return await _repository.AddAulaAsync(aula);
        }

        public async Task UpdateAsync(int id, Aula aula)
        {
            if (await _repository.Get(id) == null)
                throw new KeyNotFoundException("Aula não encontrada.");

            await _repository.UpdateAulaAsync(aula);
        }

        public async Task DeleteAsync(int id)
        {
            if (await _repository.Get(id) == null)
                throw new KeyNotFoundException("Aula não encontrada.");

            await _repository.DeleteAulaAsync(id);
        }
    }
}
