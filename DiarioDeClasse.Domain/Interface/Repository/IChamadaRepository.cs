using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Domain.Interface.Repository
{
    public interface IChamadaRepository : IBaseRepository<Chamada, int>
    {
        Task<IEnumerable<Chamada>> GetByAulaIdAsync(int aulaId);
        Task<IEnumerable<Chamada>> GetByAlunoIdAsync(int alunoId);
        Task<Chamada> AddChamadaAsync(Chamada chamada);
        Task UpdateChamadaAsync(Chamada chamada);
        Task DeleteChamadaAsync(int id);
    }
}
