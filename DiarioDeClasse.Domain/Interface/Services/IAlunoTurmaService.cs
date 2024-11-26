using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface IAlunoTurmaService
    {
        Task<IEnumerable<AlunoTurma>> GetByAlunoIdAsync(int alunoId);
        Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(int turmaId);
        Task<bool> AlunoJaMatriculadoAsync(int alunoId, int turmaId);
        Task AddAsync(AlunoTurma alunoTurma);
        Task UpdateAsync(int id, AlunoTurma alunoTurma);
        Task DeleteAsync(int id);
    }
}
