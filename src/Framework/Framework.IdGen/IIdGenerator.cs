namespace Framework.IdGen;

public interface IIdGenerator
{
    long GenerateId();
    string GenerateHashId(int id);
    string GenerateHashId(long id);
}