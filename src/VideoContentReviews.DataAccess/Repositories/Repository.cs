using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VideoContentReviews.DataAccess.Context;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    private readonly IDbContextFactory<VideoContentReviewsDbContext> _contextFactory;

    public Repository(IDbContextFactory<VideoContentReviewsDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IEnumerable<T> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var context = await _contextFactory.CreateDbContextAsync();
        return context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        var context =  await _contextFactory.CreateDbContextAsync();
        return context.Set<T>().Where(filter);
    }

    public T? GetById(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().FirstOrDefault(x => x.Id == id);
    }

   
    public async Task<T?> GetByIdAsync(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public T? GetById(Guid id)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Set<T>().FirstOrDefault(x => x.ExternalId == id);
    }
    
    public async Task<T?> GetByIdAsync(Guid externalId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Set<T>().FirstOrDefaultAsync(x => x.ExternalId == externalId);
    }

    public T Save(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        if (context.Set<T>().Any(x => x.Id == entity.Id)) //update
        {
            entity.ModificationTime = DateTime.UtcNow;
            var result = context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return result.Entity;
        }
        else //insert
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = entity.CreationTime;
            var result = context.Set<T>().Add(entity);
            context.SaveChanges();
            return result.Entity;
        }
    }

    public async Task<T> SaveAsync(T entity)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        
        var exists = await context.Set<T>().AnyAsync(x => x.Id == entity.Id);
        
        if (exists) 
        {
            entity.ModificationTime = DateTime.UtcNow;
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        else 
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = entity.CreationTime;
            await context.Set<T>().AddAsync(entity);
        }
        
        await context.SaveChangesAsync();
        return entity;
    }

    public void Delete(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Deleted;
        context.SaveChanges();
    }

    public async Task DeleteAsync(T entity)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.Set<T>().Attach(entity);
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }
}