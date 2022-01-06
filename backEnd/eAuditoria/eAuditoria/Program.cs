using eAuditoria.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer("server = localhost; database = eAuditoria; uid = thiago; pwd = palmeiras51; port = 3306"));
builder.Services.AddDbContext<DataContext>(opt => opt.UseMySql("server=localhost; port=3306; database=eAuditoria; uid=thiago; password=palmeiras51", new MySqlServerVersion(new Version(8, 0, 27))));
builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
