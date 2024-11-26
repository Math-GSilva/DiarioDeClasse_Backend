using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Domain.Interface.Repository
{
    public interface IAulaRepository : IBaseRepository<Aula, int>
    {
        Task<IEnumerable<Aula>> GetByTurmaIdAsync(int turmaId);
        Task<IEnumerable<Aula>> GetByDateAsync(DateTime date);
        Task<Aula> AddAulaAsync(Aula aula);
        Task UpdateAulaAsync(Aula aula);
        Task DeleteAulaAsync(int id);
    }
}
