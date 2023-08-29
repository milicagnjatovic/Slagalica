using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.Data;

public class DesignTimeApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) 
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true) 
            .Build();
        
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString")); //"Server=localhost;Database=IdentityDb;User Id=sa;Password=MATF12345678rs2;");
        return new ApplicationContext(optionsBuilder.Options);
    }
}