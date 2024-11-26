using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;

namespace DiarioDeClasse.Repository
{
    public class AlunoTurmaRepository : BaseRepository<AlunoTurma, int>, IAlunoTurmaRepository
    {
        public AlunoTurmaRepository(DiarioDeClasseContext db)
            : base(db)
        {
        }
        public async Task<IEnumerable<AlunoTurma>> GetByAlunoIdAsync(int alunoId)
        {
            var results = await GetAll();
            return results.Where(at => at.AlunoId == alunoId);
        }

        public async Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(int turmaId)
        {
            var results = await GetAll();
            return results.Where(at => at.TurmaId == turmaId);
        }

        public async Task<bool> AlunoJaMatriculadoAsync(int alunoId, int turmaId)
        {
            var results = await GetAll();
            return results.Any(at => at.AlunoId == alunoId && at.TurmaId == turmaId);
        }

        public async Task<AlunoTurma> AddAlunoTurmaAsync(AlunoTurma alunoTurma)
        {
            return await Add(alunoTurma);
        }

        public async Task UpdateAlunoTurmaAsync(AlunoTurma alunoTurma)
        {
            await Update(alunoTurma);
        }

        public async Task DeleteAlunoTurmaAsync(int id)
        {
            await Delete(id);
        }
    }
}
