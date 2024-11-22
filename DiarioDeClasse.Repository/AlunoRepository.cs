using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiarioDeClasse.Repository
{
    public class AlunoRepository : BaseRepository<Aluno, int>, IAlunoRepository
    {
        public AlunoRepository(DiarioDeClasseContext db)
            : base(db)
        {
        }

        public async Task<Aluno?> GetByIdWithIncludesAsync(int id, IEnumerable<string> includes)
        {
            return await Get(id, includes);
        }

        public async Task<Aluno?> GetById(int id)
        {
            return await Get(id);
        }

        public async Task<IEnumerable<Aluno>> GetByNomeAsync(string nome)
        {
            var results = await GetAll();
            return results.Where(aluno => aluno.Nome != null &&
                                          aluno.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Aluno> AddAlunoAsync(Aluno aluno)
        {
            return await Add(aluno);
        }

        public async Task UpdateAlunoAsync(Aluno aluno)
        {
            await Update(aluno);
        }

        public async Task DeleteAlunoAsync(int id)
        {
            await Delete(id);
        }
    }
}
