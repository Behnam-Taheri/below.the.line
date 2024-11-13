using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

namespace Framework.Persistence
{
    public static class MigrationBuilderExtensions
    {
        public static void SqlResource(this MigrationBuilder migrationBuilder, string resourceFullPath)
        {
            var assembly = Assembly.GetCallingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceFullPath);
            using var reader = new StreamReader(stream);
            var script = reader.ReadToEnd();

            migrationBuilder.Sql(script);
        }
    }
}
