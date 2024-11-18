using DiarioDeClasse.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Repository
{
    public interface IProfessorRepository : IBaseRepository<Professor, int>
    {
        Task<Professor?> GetByIdWithIncludesAsync(int id, IEnumerable<string> includes);
        Task<Professor?> GetById(int id);
        Task<IEnumerable<Professor>> GetByDisciplinaAsync(string disciplina);
        Task<Professor> AddProfessorAsync(Professor professor);
        Task UpdateProfessorAsync(Professor professor);
        Task DeleteProfessorAsync(int id);
    }
}
