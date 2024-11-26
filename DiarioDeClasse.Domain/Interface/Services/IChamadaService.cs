using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface IChamadaService
    {
        Task<IEnumerable<Chamada>> GetByAulaIdAsync(int aulaId);
        Task<IEnumerable<Chamada>> GetByAlunoIdAsync(int alunoId);
        Task<Chamada> AddAsync(Chamada chamada);
        Task UpdateAsync(int id, Chamada chamada);
        Task DeleteAsync(int id);
    }
}
