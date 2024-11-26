using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Domain.Interface.Repository
{
    public interface INotaRepository : IBaseRepository<Nota, int>
    {
        Task<IEnumerable<Nota>> GetByAlunoIdAsync(int alunoId);
        Task<IEnumerable<Nota>> GetByTurmaIdAsync(int turmaId);
        Task<IEnumerable<Nota>> GetByAvaliacaoAsync(string avaliacao);
        Task<Nota> AddNotaAsync(Nota nota);
        Task UpdateNotaAsync(Nota nota);
        Task DeleteNotaAsync(int id);
    }
}
