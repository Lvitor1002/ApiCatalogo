
using ApiCatalogo.ContextDB;
using Microsoft.EntityFrameworkCore;






var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogoDbContext>(op =>
                                                op.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));







var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.MapOpenApi();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
