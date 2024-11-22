using DiarioDeClasse.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAllAsync();
        Task<Aluno?> GetByIdAsync(int id);
        Task AddAsync(Aluno aluno);
        Task UpdateAsync(int id, Aluno aluno);
        Task DeleteAsync(int id);
    }
}
