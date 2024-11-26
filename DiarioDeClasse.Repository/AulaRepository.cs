using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;

namespace DiarioDeClasse.Repository
{
    public class AulaRepository : BaseRepository<Aula, int>, IAulaRepository
    {
        public AulaRepository(DiarioDeClasseContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<Aula>> GetByTurmaIdAsync(int turmaId)
        {
            var results = await GetAll();
            return results.Where(aula => aula.TurmaId == turmaId);
        }

        public async Task<IEnumerable<Aula>> GetByDateAsync(DateTime date)
        {
            var results = await GetAll();
            return results.Where(aula => aula.Data.Date == date.Date);
        }

        public async Task<Aula> AddAulaAsync(Aula aula)
        {
            return await Add(aula);
        }

        public async Task UpdateAulaAsync(Aula aula)
        {
            await Update(aula);
        }

        public async Task DeleteAulaAsync(int id)
        {
            await Delete(id);
        }
    }
}
