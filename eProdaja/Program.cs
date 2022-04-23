using eProdaja.Controllers;
using eProdaja.Services;
using eProdaja.Services.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//implementacija auto mappera
builder.Services.AddAutoMapper(typeof(IKorisniciService));
//sva mapiranja iz Databasea gdje se nalaze sve klase, u nas Model ce se nalaziti u projektu eProdaja.Services
//potrebno je da imamo bilo kakvu klasu ili interfejs referenciranu kroz deklaraciju iznad (AddAutoMapper);


//za interface IProizvodiService implementacija ce mu biti ProizvodiService a unutar nje se Geta iz baze podataka
builder.Services.AddTransient<IProizvodiService,ProizvodiService>();
//builder.Services.AddSingleton<IProizvodiService, ProizvodiService>();

builder.Services.AddTransient<IKorisniciService, KorisniciService>();
builder.Services.AddTransient<IJedinicaMjereService, JedinicaMjereService>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<eProdajaContext>(options=>
options.UseSqlServer(connection));

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
