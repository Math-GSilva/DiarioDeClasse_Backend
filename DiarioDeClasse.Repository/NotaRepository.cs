using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;

namespace DiarioDeClasse.Repository
{
    public class NotaRepository : BaseRepository<Nota, int>, INotaRepository
    {
        public NotaRepository(DiarioDeClasseContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<Nota>> GetByAlunoIdAsync(int alunoId)
        {
            var results = await GetAll();
            return results.Where(nota => nota.AlunoId == alunoId);
        }

        public async Task<IEnumerable<Nota>> GetByTurmaIdAsync(int turmaId)
        {
            var results = await GetAll();
            return results.Where(nota => nota.TurmaId == turmaId);
        }

        public async Task<IEnumerable<Nota>> GetByAvaliacaoAsync(string avaliacao)
        {
            var results = await GetAll();
            return results.Where(nota => nota.Avaliacao != null && nota.Avaliacao.Equals(avaliacao, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Nota> AddNotaAsync(Nota nota)
        {
            return await Add(nota);
        }

        public async Task UpdateNotaAsync(Nota nota)
        {
            await Update(nota);
        }

        public async Task DeleteNotaAsync(int id)
        {
            await Delete(id);
        }
    }
}
