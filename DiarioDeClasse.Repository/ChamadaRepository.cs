using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;

namespace DiarioDeClasse.Repository
{
    public class ChamadaRepository : BaseRepository<Chamada, int>, IChamadaRepository
    {
        public ChamadaRepository(DiarioDeClasseContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<Chamada>> GetByAulaIdAsync(int aulaId)
        {
            var results = await GetAll();
            return results.Where(chamada => chamada.AulaId == aulaId);
        }

        public async Task<IEnumerable<Chamada>> GetByAlunoIdAsync(int alunoId)
        {
            var results = await GetAll();
            return results.Where(chamada => chamada.AlunoId == alunoId);
        }

        public async Task<Chamada> AddChamadaAsync(Chamada chamada)
        {
            return await Add(chamada);
        }

        public async Task UpdateChamadaAsync(Chamada chamada)
        {
            await Update(chamada);
        }

        public async Task DeleteChamadaAsync(int id)
        {
            await Delete(id);
        }
    }
}
