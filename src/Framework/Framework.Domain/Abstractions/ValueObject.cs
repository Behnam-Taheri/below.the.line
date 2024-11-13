namespace Framework.Domain.Abstractions
{
    public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
    {
        public bool Equals(T? other) => this == other;
        public override bool Equals(object? other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (other is ValueObject<T> otherObject)
                return GetEqualityComponents().SequenceEqual(otherObject.GetEqualityComponents());

            return false;
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right) => left.Equals(right);
        public static bool operator !=(ValueObject<T> left, ValueObject<T> right) => !(left == right);
        protected abstract IEnumerable<object> GetEqualityComponents();
    }
}
