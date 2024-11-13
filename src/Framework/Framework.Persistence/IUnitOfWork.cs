namespace Framework.Persistence
{
    public interface IUnitOfWork
    {
        Task BeginAsync(CancellationToken cancellationToken = default);
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RolBackAsync(CancellationToken cancellationToken = default);
    }
}
