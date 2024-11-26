using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface IAulaService
    {
        Task<IEnumerable<Aula>> GetByTurmaIdAsync(int turmaId);
        Task<IEnumerable<Aula>> GetByDateAsync(DateTime date);
        Task<Aula> AddAsync(Aula aula);
        Task UpdateAsync(int id, Aula aula);
        Task DeleteAsync(int id);
    }
}
