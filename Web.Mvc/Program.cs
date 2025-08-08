using InfraData.Context;
using Infrastructure.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os controllers
builder.Services.AddControllers();

// Configurar a autenticação Bearer com JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // Teste local sem HTTPS
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "unique-app-id-12345",
            ValidAudience = "MyAppUsers",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gF7!hAq$8zQzP@5r7Hk1zLk9I0gT4uYv"))
        };
    });

// Configura a conexão com o banco de dados MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Adiciona o Swagger para a documentação da API, incluindo autenticação JWT
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "EcomApi", Version = "v1" });

    // Adiciona suporte à autenticação JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira apenas o token JWT, sem o prefixo 'Bearer'. O prefixo será adicionado automaticamente."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()  // Permite qualquer origem
                  .AllowAnyMethod()   // Permite qualquer método HTTP
                  .AllowAnyHeader();  // Permite qualquer cabeçalho
        });
});



// Injeta dependências da infraestrutura
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configuração do Swagger e Https
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll"); 
app.UseHttpsRedirection(); // Certifique-se de que vem antes da autenticação/autorização

app.UseAuthentication(); // Ativa autenticação JWT
app.UseAuthorization();  // Ativa autorização para proteger rotas

app.MapControllers(); // Mapeia os endpoints da API
app.UseStaticFiles(); 

app.Run();
