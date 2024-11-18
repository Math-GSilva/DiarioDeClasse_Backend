using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Service
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _repository;

        public ProfessorService(IProfessorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Professor>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<Professor?> GetByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task AddAsync(Professor professor)
        {
            await _repository.AddProfessorAsync(professor);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Professor professor)
        {
            var existingProfessor = await _repository.GetById(id);
            if (existingProfessor == null)
                throw new KeyNotFoundException("Professor not found.");

            existingProfessor.Nome = professor.Nome;
            existingProfessor.Disciplina = professor.Disciplina;
            existingProfessor.UsuarioId = professor.UsuarioId;

            await _repository.Update(existingProfessor);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var professor = await _repository.GetById(id);
            if (professor == null)
                throw new KeyNotFoundException("Professor not found.");

            await _repository.DeleteProfessorAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}
