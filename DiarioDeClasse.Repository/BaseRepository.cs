using DiarioDeClasse.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Repository
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly DbContext _db;

        protected BaseRepository(DbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAll(string include)
        {
            return await _db.Set<TEntity>().Include(include).ToListAsync();
        }

        public async Task<List<TEntity>> GetAll(IEnumerable<string> includes)
        {
            var query = _db.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> Get(TKey id)
        {
            return await _db.Set<TEntity>().SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<TEntity?> Get(TKey id, string include)
        {
            return await _db.Set<TEntity>().Include(include).SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<TEntity?> Get(TKey id, IEnumerable<string> includes)
        {
            var query = _db.Set<TEntity>().AsQueryable();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.SingleOrDefaultAsync(c => c.Id.Equals(id));
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await _db.Set<TEntity>().AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding entity", ex);
            }
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                await _db.Set<TEntity>().AddRangeAsync(entities);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding entities", ex);
            }
        }

        public virtual async Task Delete(TKey id)
        {
            try
            {
                var entity = new TEntity { Id = id };
                _db.Set<TEntity>().Attach(entity);
                _db.Set<TEntity>().Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting entity", ex);
            }
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync()) > 0;
        }

        public virtual async Task Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _db.Set<TEntity>().Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating entity", ex);
            }
        }
    }
}
