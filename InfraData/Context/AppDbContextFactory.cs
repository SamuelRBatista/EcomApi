using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.IO;

namespace InfraData.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Carrega a string de conexão do appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Web.Mvc")) // Caminho relativo para Web.Mvc
            .AddJsonFile("appsettings.json") // Adiciona o arquivo de configuração
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configura o provedor do MySQL
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new AppDbContext(optionsBuilder.Options);
        }
    }
}