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

        // Método para obter todas as relações de um aluno em turmas
        public async Task<IEnumerable<AlunoTurma>> GetByAlunoIdAsync(int alunoId)
        {
            var results = await GetAll();
            return results.Where(at => at.AlunoId == alunoId);
        }

        // Método para obter todas as relações de uma turma com alunos
        public async Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(int turmaId)
        {
            var results = await GetAll();
            return results.Where(at => at.TurmaId == turmaId);
        }

        // Método para verificar se um aluno já está matriculado em uma turma
        public async Task<bool> AlunoJaMatriculadoAsync(int alunoId, int turmaId)
        {
            var results = await GetAll();
            return results.Any(at => at.AlunoId == alunoId && at.TurmaId == turmaId);
        }

        // Método para adicionar uma nova relação Aluno-Turma
        public async Task<AlunoTurma> AddAlunoTurmaAsync(AlunoTurma alunoTurma)
        {
            return await Add(alunoTurma);
        }

        // Método para atualizar a relação Aluno-Turma (ex.: Data de Saída)
        public async Task UpdateAlunoTurmaAsync(AlunoTurma alunoTurma)
        {
            await Update(alunoTurma);
        }

        // Método para deletar uma relação Aluno-Turma
        public async Task DeleteAlunoTurmaAsync(int id)
        {
            await Delete(id);
        }
    }
}
