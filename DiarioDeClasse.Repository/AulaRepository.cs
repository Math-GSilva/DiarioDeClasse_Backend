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

        // Método para obter todas as aulas de uma turma
        public async Task<IEnumerable<Aula>> GetByTurmaIdAsync(int turmaId)
        {
            var results = await GetAll();
            return results.Where(aula => aula.TurmaId == turmaId);
        }

        // Método para obter aulas em uma determinada data
        public async Task<IEnumerable<Aula>> GetByDateAsync(DateTime date)
        {
            var results = await GetAll();
            return results.Where(aula => aula.Data.Date == date.Date);
        }

        // Método para adicionar uma nova aula
        public async Task<Aula> AddAulaAsync(Aula aula)
        {
            return await Add(aula);
        }

        // Método para atualizar uma aula
        public async Task UpdateAulaAsync(Aula aula)
        {
            await Update(aula);
        }

        // Método para deletar uma aula
        public async Task DeleteAulaAsync(int id)
        {
            await Delete(id);
        }
    }
}
