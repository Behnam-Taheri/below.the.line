namespace Framework.Core.Abstractions
{
    public class Entity<TKey>
    {
        public TKey Id { get; protected set; }
        public DateTime CreateDate { get; protected set; }
    }
}
