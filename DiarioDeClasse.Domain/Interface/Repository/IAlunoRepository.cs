using DiarioDeClasse.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Repository
{
    public interface IAlunoRepository : IBaseRepository<Aluno, int>
    {
        Task<Aluno?> GetByIdWithIncludesAsync(int id, IEnumerable<string> includes);
        Task<Aluno?> GetById(int id);
        Task<IEnumerable<Aluno>> GetByNomeAsync(string nome);
        Task<Aluno> AddAlunoAsync(Aluno aluno);
        Task UpdateAlunoAsync(Aluno aluno);
        Task DeleteAlunoAsync(int id);
    }
}
