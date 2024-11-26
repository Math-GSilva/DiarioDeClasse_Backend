using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface INotaService
    {
        Task<IEnumerable<Nota>> GetByAlunoIdAsync(int alunoId);
        Task<IEnumerable<Nota>> GetByTurmaIdAsync(int turmaId);
        Task<IEnumerable<Nota>> GetByAvaliacaoAsync(string avaliacao);
        Task<Nota> AddAsync(Nota nota);
        Task UpdateAsync(int id, Nota nota);
        Task DeleteAsync(int id);
    }
}
