
using System.Text.Json.Serialization;
using ApiCatalogo.ContextDB;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogoDbContext>(op =>
                                                op.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));


builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Registra o serviço genérico IRepository<T> e sua implementação Repository<T>
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();





var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
