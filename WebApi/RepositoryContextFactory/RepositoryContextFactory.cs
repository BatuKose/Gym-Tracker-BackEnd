using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EFCore;

namespace WebApi.RepositoryContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var Configuration= new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(Configuration.GetConnectionString("SqlConnection"),
            prj => prj.MigrationsAssembly("WebApi"));
            return new RepositoryContext(builder.Options);
        }
    }
}
