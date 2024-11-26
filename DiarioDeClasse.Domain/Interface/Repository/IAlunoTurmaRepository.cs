using DiarioDeClasse.Domain.Entity;

namespace DiarioDeClasse.Domain.Interface.Repository
{
    public interface IAlunoTurmaRepository : IBaseRepository<AlunoTurma, int>
    {
        Task<IEnumerable<AlunoTurma>> GetByAlunoIdAsync(int alunoId);
        Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(int turmaId);
        Task<bool> AlunoJaMatriculadoAsync(int alunoId, int turmaId);
        Task<AlunoTurma> AddAlunoTurmaAsync(AlunoTurma alunoTurma);
        Task UpdateAlunoTurmaAsync(AlunoTurma alunoTurma);
        Task DeleteAlunoTurmaAsync(int id);
    }
}
