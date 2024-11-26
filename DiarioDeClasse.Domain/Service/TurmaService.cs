using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Interface.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Service
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _repository;

        public TurmaService(ITurmaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Turma>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<Turma?> GetByIdAsync(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<Turma> AddAsync(Turma turma)
        {
            var createdTurma = await _repository.AddTurmaAsync(turma);
            await _repository.SaveChangesAsync();
            return createdTurma;
        }

        public async Task UpdateAsync(int id, Turma turma)
        {
            var existingTurma = await _repository.Get(id);
            if (existingTurma == null)
                throw new KeyNotFoundException("Turma not found.");

            existingTurma.Nome = turma.Nome;
            existingTurma.AnoLetivo = turma.AnoLetivo;
            existingTurma.ProfessorId = turma.ProfessorId;

            await _repository.Update(existingTurma);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var turma = await _repository.Get(id);
            if (turma == null)
                throw new KeyNotFoundException("Turma not found.");

            await _repository.DeleteTurmaAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}
