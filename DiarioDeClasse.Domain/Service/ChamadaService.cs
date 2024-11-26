using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Interface.Services;

namespace DiarioDeClasse.Domain.Service
{
    public class ChamadaService : IChamadaService
    {
        private readonly IChamadaRepository _repository;

        public ChamadaService(IChamadaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Chamada>> GetByAulaIdAsync(int aulaId)
        {
            return await _repository.GetByAulaIdAsync(aulaId);
        }

        public async Task<IEnumerable<Chamada>> GetByAlunoIdAsync(int alunoId)
        {
            return await _repository.GetByAlunoIdAsync(alunoId);
        }

        public async Task<Chamada> AddAsync(Chamada chamada)
        {
            return await _repository.AddChamadaAsync(chamada);
        }

        public async Task UpdateAsync(int id, Chamada chamada)
        {
            if (await _repository.Get(id) == null)
                throw new KeyNotFoundException("Chamada não encontrada.");

            await _repository.UpdateChamadaAsync(chamada);
        }

        public async Task DeleteAsync(int id)
        {
            if (await _repository.Get(id) == null)
                throw new KeyNotFoundException("Chamada não encontrada.");

            await _repository.DeleteChamadaAsync(id);
        }
    }
}
