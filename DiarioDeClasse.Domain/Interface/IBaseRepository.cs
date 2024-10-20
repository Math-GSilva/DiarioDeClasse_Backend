using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Domain.Interface
{
    public interface IBaseRepository<TEntity, in TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(string include);
        Task<List<TEntity>> GetAll(IEnumerable<string> includes);
        Task<TEntity?> Get(TKey id);
        Task<TEntity?> Get(TKey id, string include);
        Task<TEntity?> Get(TKey id, IEnumerable<string> includes);
        Task<TEntity> Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task Delete(TKey id);
        Task Update(TEntity entity);
        Task<bool> SaveChangesAsync();
    }
}
