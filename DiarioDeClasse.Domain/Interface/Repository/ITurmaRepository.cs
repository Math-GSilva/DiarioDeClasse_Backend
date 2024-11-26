using DiarioDeClasse.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Repository
{
    public interface ITurmaRepository : IBaseRepository<Turma, int>
    {
        Task<Turma?> GetByIdWithIncludesAsync(int id, IEnumerable<string> includes);
        Task<IEnumerable<Turma>> GetByAnoLetivoAsync(int anoLetivo);
        Task<Turma> AddTurmaAsync(Turma turma);
        Task UpdateTurmaAsync(Turma turma);
        Task DeleteTurmaAsync(int id);
    }
}
