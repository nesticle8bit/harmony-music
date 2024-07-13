using System.Linq.Expressions;
using Harmony.Music.Contracts.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace Harmony.Music.Repository.RepositoryBase;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext;

    protected RepositoryBase(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;

    public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? RepositoryContext.Set<T>().AsNoTracking() : RepositoryContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ? RepositoryContext.Set<T>().Where(expression).AsNoTracking() : RepositoryContext.Set<T>().Where(expression);

    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

    public void CreateAsync(T entity) => RepositoryContext.Set<T>().AddAsync(entity);
    
    public void CreateRange(T entity) => RepositoryContext.Set<T>().AddRange(entity);

    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    
    public void UpdateRange(T entity) => RepositoryContext.Set<T>().UpdateRange(entity);

    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    
    public void DeleteRange(T entity) => RepositoryContext.Set<T>().RemoveRange(entity);
}