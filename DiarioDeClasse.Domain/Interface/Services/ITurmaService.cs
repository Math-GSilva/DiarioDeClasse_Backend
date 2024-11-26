using DiarioDeClasse.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface ITurmaService
    {
        Task<IEnumerable<Turma>> GetAllAsync();
        Task<Turma?> GetByIdAsync(int id);
        Task<Turma> AddAsync(Turma turma);
        Task UpdateAsync(int id, Turma turma);
        Task DeleteAsync(int id);
    }
}
