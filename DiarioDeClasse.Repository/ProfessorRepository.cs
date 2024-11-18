using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using DiarioDeClasse.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Repository
{
    public class ProfessorRepository : BaseRepository<Professor, int>, IProfessorRepository
    {
        public ProfessorRepository(DiarioDeClasseContext db)
            : base(db)
        {
        }

        public async Task<Professor?> GetByIdWithIncludesAsync(int id, IEnumerable<string> includes)
        {
            return await Get(id, includes);
        }

        public async Task<Professor?> GetById(int id)
        {
            return await Get(id);
        }

        public async Task<IEnumerable<Professor>> GetByDisciplinaAsync(string disciplina)
        {
            var results = await GetAll();
            return results.Where(prof => prof.Disciplina.Equals(disciplina, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Professor> AddProfessorAsync(Professor professor)
        {
            return await Add(professor);
        }

        public async Task UpdateProfessorAsync(Professor professor)
        {
            await Update(professor);
        }

        public async Task DeleteProfessorAsync(int id)
        {
            await Delete(id);
        }
    }
}
