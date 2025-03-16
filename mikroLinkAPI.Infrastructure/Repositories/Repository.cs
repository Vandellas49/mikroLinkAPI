using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using mikroLinkAPI.Application.Features.Materials.MetarialIn;
using mikroLinkAPI.Domain.ViewModel;
using System.Linq.Expressions;

namespace GenericRepository;

public class Repository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class, new()
    where TContext : DbContext
{
    private readonly TContext _context;
    private DbSet<TEntity> Entity;
    private readonly IMemoryCache _memoryCache;

    public Repository(TContext context, IMemoryCache memoryCache)
    {
        _context = context;
        Entity = _context.Set<TEntity>();
        _memoryCache = memoryCache;
    }

    public void Add(TEntity entity)
    {
        Entity.Add(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entity.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Entity.AddRangeAsync(entities, cancellationToken);
    }

    public bool Any(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.Any(expression);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Entity.AnyAsync(expression, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        Entity.Remove(entity);
    }

    public async Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        var entities = await Entity.Where(expression).AsNoTracking().ToListAsync(cancellationToken);
        if (entities != null && entities.Count > 0)
            Entity.RemoveRange(entities);
    }

    public async Task DeleteByIdAsync(string id)
    {
        TEntity entity = await Entity.FindAsync(id);
        Entity.Remove(entity);
    }

    public void DeleteRange(ICollection<TEntity> entities)
    {
        Entity.RemoveRange(entities);
    }

    public IQueryable<TEntity> GetAll()
    {
        return Entity.AsNoTracking().AsQueryable();
    }

    public IQueryable<TEntity> GetAllWithTracking()
    {
        return Entity.AsQueryable();
    }

    public TEntity GetByExpression(Expression<Func<TEntity, bool>> expression)
    {
        TEntity entity = Entity.Where(expression).AsNoTracking().FirstOrDefault();
        return entity;
    }

    public async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, bool isTrackingActive = true)
    {
        TEntity entity;
        if (isTrackingActive)
        {
            entity = await Entity.Where(expression).FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            entity = await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        return entity;
    }


    public TEntity GetByExpressionWithTracking(Expression<Func<TEntity, bool>> expression)
    {
        TEntity entity = Entity.Where(expression).FirstOrDefault();
        return entity;
    }

    public async Task<TEntity> GetByExpressionWithTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.Where(expression).FirstOrDefaultAsync(cancellationToken);
        return entity;
    }

    public TEntity GetFirst(Expression<Func<TEntity, bool>> expression)
    {
        TEntity entity = Entity.AsNoTracking().FirstOrDefault(expression);
        return entity;
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
        return entity;
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.AsNoTracking().Where(expression).AsQueryable();
    }

    public IQueryable<TEntity> WhereWithTracking(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.Where(expression).AsQueryable();
    }
    public IQueryable<TEntity> Includes(params Expression<Func<TEntity, object>>[] includes)
    {
        var query = Entity.AsQueryable();
        foreach (var item in includes)
        {
            query = query.Include(item);
        }
        return query;
    }
    public void Update(TEntity entity)
    {
        Entity.Update(entity);
    }

    public void UpdateRange(ICollection<TEntity> entities)
    {
        Entity.UpdateRange(entities);
    }

    public void AddRange(ICollection<TEntity> entities)
    {
        Entity.AddRange(entities);
    }

    public void BulkInsertEntities(List<TEntity> entities)
    {
        _context.BulkInsert(entities);
    }

    public async Task BulkInsertEntitiesAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _context.BulkInsertAsync(entities, cancellationToken: cancellationToken);
    }
    public async Task<List<T>> PureSqlCommandAsync<T>(string query)
    {
        _context.Database.SetCommandTimeout(1000000);
        return await _context.Database.SqlQuery<T>($"{query}").ToListAsync();
    }
    public async Task<List<TEntity>> PureSqlCommandAsync(string query, params object[] parameters)
    {
        _context.Database.SetCommandTimeout(1000000);
        return await Entity.FromSqlRaw(query, parameters).ToListAsync();
    }
    public async Task<DbCount> PureSqlCommandCountAsync(string query, params object[] parameters)
    {
        _context.Database.SetCommandTimeout(1000000);
        return await _context.Database.SqlQueryRaw<DbCount>(query, parameters).FirstOrDefaultAsync();
    }
    public List<T> PureSqlCommand<T>(string query)
    {
        _context.Database.SetCommandTimeout(1000000);
        return _context.Database.SqlQuery<T>($"{query}").ToList();
    }
    public async Task<int> PureSqlCommandExecuteAsync(string query)
    {
        _context.Database.SetCommandTimeout(1000000);
        return await _context.Database.ExecuteSqlRawAsync(query);
    }

    public List<TEntity> GetAllFromCache()
    {
        if (!_memoryCache.TryGetValue(typeof(TEntity).Name, out List<TEntity> result))
        {
            result = Entity.ToList();
            if (result != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _memoryCache.Set(typeof(TEntity).Name, result, cacheEntryOptions);
            }
        }
        return result;
    }
    public Task<List<TEntity>> GetAllFromCacheAsync()
    {
        if (!_memoryCache.TryGetValue(typeof(TEntity).Name, out List<TEntity> result))
        {
            result = Entity.ToList();
            if (result != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _memoryCache.Set(typeof(TEntity).Name, result, cacheEntryOptions);
            }
        }
        return Task.FromResult(result);
    }
    public List<TEntity> GetAllFromCache(Func<TEntity, bool> expression)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
        if (!_memoryCache.TryGetValue(typeof(TEntity).Name, out List<TEntity> result))
        {
            result = Entity.Where(expression).ToList();
            if (result != null)
            {
                _memoryCache.Set(typeof(TEntity).Name, result, cacheEntryOptions);
            }
        }
        var data = result.Where(expression).ToList();
        if (data == null || data.Count == 0)
        {
            data = Entity.Where(expression).ToList();
            result.AddRange(data);
            _memoryCache.Set(typeof(TEntity).Name, result, cacheEntryOptions);
        }
        return data;
    }
    public Task<List<TEntity>> GetAllFromCacheAsync(Func<TEntity, bool> expression)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
        if (!_memoryCache.TryGetValue(typeof(TEntity).Name, out List<TEntity> result))
        {
            result = Entity.Where(expression).Where(p => p != null).ToList();
            if (result != null)
            {
                _memoryCache.Set(typeof(TEntity).Name, result, cacheEntryOptions);
            }
        }
        var data = result.Where(p => p != null).Where(expression).ToList();
        if (data == null || data.Count == 0)
        {
            data = Entity.Where(expression).ToList();
            if (data != null)
                result.AddRange(data);
            _memoryCache.Set(typeof(TEntity).Name, result, cacheEntryOptions);
        }
        return Task.FromResult(data);
    }


}