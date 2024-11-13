using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framework.Persistence;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    protected readonly DbContext context;

    protected BaseRepository(DbContext dbContext)
    {
        context = dbContext;
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return context.Set<TEntity>().Count(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>().CountAsync(predicate, cancellationToken: cancellationToken);
    }

    public TEntity? Get(Expression<Func<TEntity?, bool>> predicate)
    {
        var aggregate = ConvertToAggregate(context.Set<TEntity>());
        return aggregate.FirstOrDefault(predicate);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var aggregate = ConvertToAggregate(context.Set<TEntity>());
        return await aggregate.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public List<TEntity?> GetList(Expression<Func<TEntity?, bool>> predicate)
    {
        var aggregate = ConvertToAggregate(context.Set<TEntity>());
        return aggregate.Where(predicate).ToList();
    }


    public List<TEntity?> GetAll()
    {
        var aggregate = ConvertToAggregate(context.Set<TEntity>());
        return aggregate.ToList();
    }

    public async Task<List<TEntity?>> GetAllAsync(CancellationToken cancellationToken)
    {
        var aggregate = ConvertToAggregate(context.Set<TEntity>());
        return await aggregate.ToListAsync(cancellationToken: cancellationToken);
    }

    public bool IsExist(Expression<Func<TEntity, bool>> predicate)
    {
        return context.Set<TEntity>().Any(predicate);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await context.Set<TEntity>().AnyAsync(predicate, cancellationToken: cancellationToken);
    }

    public void Create(TEntity aggregate)
    {
        context.Set<TEntity>().Add(aggregate);
        context.SaveChanges();
    }

    public async Task CreateAsync(TEntity aggregate, CancellationToken cancellationToken)
    {
        await context.Set<TEntity>().AddAsync(aggregate, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public void Delete(TEntity aggregate)
    {
        context.Set<TEntity>().Remove(aggregate);
    }

    public async Task DeleteAsync(TEntity aggregate, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Remove(aggregate);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity aggregate, CancellationToken cancellationToken)
    {
        context.Set<TEntity>().Update(aggregate);
        Console.WriteLine(context.ChangeTracker.DebugView.LongView);
        await context.SaveChangesAsync(cancellationToken);
    }

    public TEntity? GetOrDefault(Expression<Func<TEntity?, bool>> predicate)
    {
        var aggregate = ConvertToAggregate(context.Set<TEntity>());
        return aggregate.SingleOrDefault(predicate);
    }

    public async Task<TEntity?> GetOrDefaultAsync(Expression<Func<TEntity?, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var aggregate = ConvertToAggregate(context.Set<TEntity>());
        return await aggregate.SingleOrDefaultAsync(predicate, cancellationToken: cancellationToken);
    }

    public void SaveChanges() => context.SaveChanges();

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    protected abstract IEnumerable<Expression<Func<TEntity, object>>>? GetIncludeExpressions();

    private IQueryable<TEntity?> ConvertToAggregate(IQueryable<TEntity?> set)
    {
        var result = set;
        var includeExpressions = GetIncludeExpressions();

        if (includeExpressions != null)
        {
            foreach (var expression in includeExpressions)
            {
                result = NestedInclude(result, expression);
            }
        }
        return result;
    }

    private IQueryable<TEntity> NestedInclude<TEntity>(IQueryable<TEntity?> query, Expression<Func<TEntity, object>> selector) where TEntity : class
    {
        string path = new PropertyPathVisitor().GetPropertyPath(selector);
        return query.Include(path);
    }
}