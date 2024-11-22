using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Interface.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Service
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;

        public AlunoService(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Aluno>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<Aluno?> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task AddAsync(Aluno aluno)
        {
            await _repository.AddAlunoAsync(aluno);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Aluno aluno)
        {
            var existingAluno = await _repository.GetById(id);
            if (existingAluno == null)
                throw new KeyNotFoundException("Aluno not found.");

            existingAluno.Nome = aluno.Nome;
            existingAluno.Matricula = aluno.Matricula;
            existingAluno.DataNascimento = aluno.DataNascimento;

            await _repository.Update(existingAluno);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var aluno = await _repository.GetById(id);
            if (aluno == null)
                throw new KeyNotFoundException("Aluno not found.");

            await _repository.DeleteAlunoAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}
