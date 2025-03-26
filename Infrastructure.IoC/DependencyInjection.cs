using Application.Services;
using Domain.Interfaces;
using Domain.Entities;
using InfraData.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces;
using Application.Validations;


namespace Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
       
            // Registrando repositórios
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IClientReposiory, ClientRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStateReposiory, StateRespository>();            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            
          
   
      
            

            // Registrando serviços
            services.AddScoped<ProductService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<CityService>();
            services.AddScoped<StateService>();
            services.AddScoped<ClientService>();
            services.AddScoped<SupplierService>();
            
            services.AddScoped<ClientValidator>(); 
      

      
 
       

        return services;
    }
}
