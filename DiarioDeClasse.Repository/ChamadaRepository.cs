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

        // Método para obter chamadas por ID da aula
        public async Task<IEnumerable<Chamada>> GetByAulaIdAsync(int aulaId)
        {
            var results = await GetAll();
            return results.Where(chamada => chamada.AulaId == aulaId);
        }

        // Método para obter chamadas por ID do aluno
        public async Task<IEnumerable<Chamada>> GetByAlunoIdAsync(int alunoId)
        {
            var results = await GetAll();
            return results.Where(chamada => chamada.AlunoId == alunoId);
        }

        // Método para adicionar uma nova chamada
        public async Task<Chamada> AddChamadaAsync(Chamada chamada)
        {
            return await Add(chamada);
        }

        // Método para atualizar uma chamada
        public async Task UpdateChamadaAsync(Chamada chamada)
        {
            await Update(chamada);
        }

        // Método para deletar uma chamada
        public async Task DeleteChamadaAsync(int id)
        {
            await Delete(id);
        }
    }
}
