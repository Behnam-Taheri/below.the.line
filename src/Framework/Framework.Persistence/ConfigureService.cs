using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Persistence;

public static class ConfigureService
{
    public static void AddPersistenceServices<T>(this IServiceCollection services) where T : DbContext
    {
        services.AddScoped<DbContext, T>();
    }
}