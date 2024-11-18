using DiarioDeClasse.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface.Services
{
    public interface IProfessorService
    {
        Task<IEnumerable<Professor>> GetAllAsync();
        Task<Professor?> GetByIdAsync(int id);
        Task AddAsync(Professor professor);
        Task UpdateAsync(int id, Professor professor);
        Task DeleteAsync(int id);
    }
}
