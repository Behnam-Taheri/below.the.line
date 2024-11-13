using HashidsNet;
using IdGen;

namespace Framework.IdGen
{
    public class FrameworkIdGenerator : IIdGenerator
    {
        private const string Salt = "this is my salt";
        public const string Characters = "abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ123456789";
        private readonly IdGenerator _idGenerator;

        public FrameworkIdGenerator(IdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public long GenerateId() => _idGenerator.CreateId();

        public string GenerateHashId(int id)
        {
            var hashId = new Hashids(Salt, 6, Characters);
            var hash = hashId.Encode(id);
            return hash;
        }

        public string GenerateHashId(long id)
        {
            var hashId = new Hashids(Salt, 8, Characters);
            var hash = hashId.EncodeLong(id);
            return hash;
        }

    }
}
