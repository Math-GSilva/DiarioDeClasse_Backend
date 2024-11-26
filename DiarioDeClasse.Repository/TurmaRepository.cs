using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiarioDeClasse.Repository
{
    public class TurmaRepository : BaseRepository<Turma, int>, ITurmaRepository
    {
        public TurmaRepository(DiarioDeClasseContext db) : base(db)
        {
        }

        public async Task<Turma?> GetByIdWithIncludesAsync(int id, IEnumerable<string> includes)
        {
            return await Get(id, includes);
        }

        public async Task<IEnumerable<Turma>> GetByAnoLetivoAsync(int anoLetivo)
        {
            var turmas = await GetAll();
            return turmas.Where(t => t.AnoLetivo == anoLetivo);
        }

        public async Task<Turma> AddTurmaAsync(Turma turma)
        {
            return await Add(turma);
        }

        public async Task UpdateTurmaAsync(Turma turma)
        {
            await Update(turma);
        }

        public async Task DeleteTurmaAsync(int id)
        {
            await Delete(id);
        }
    }
}
