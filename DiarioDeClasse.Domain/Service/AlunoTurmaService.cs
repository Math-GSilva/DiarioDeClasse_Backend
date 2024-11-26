using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Interface.Services;

namespace DiarioDeClasse.Domain.Service
{
    public class AlunoTurmaService : IAlunoTurmaService
    {
        private readonly IAlunoTurmaRepository _repository;

        public AlunoTurmaService(IAlunoTurmaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AlunoTurma>> GetByAlunoIdAsync(int alunoId)
        {
            return await _repository.GetByAlunoIdAsync(alunoId);
        }

        public async Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(int turmaId)
        {
            return await _repository.GetByTurmaIdAsync(turmaId);
        }

        public async Task<bool> AlunoJaMatriculadoAsync(int alunoId, int turmaId)
        {
            return await _repository.AlunoJaMatriculadoAsync(alunoId, turmaId);
        }

        public async Task AddAsync(AlunoTurma alunoTurma)
        {
            await _repository.AddAlunoTurmaAsync(alunoTurma);
        }

        public async Task UpdateAsync(int id, AlunoTurma alunoTurma)
        {
            if (await _repository.Get(id) == null)
                throw new KeyNotFoundException("AlunoTurma não encontrado.");

            await _repository.UpdateAlunoTurmaAsync(alunoTurma);
        }

        public async Task DeleteAsync(int id)
        {
            if (await _repository.Get(id) == null)
                throw new KeyNotFoundException("AlunoTurma não encontrado.");

            await _repository.DeleteAlunoTurmaAsync(id);
        }
    }
}
