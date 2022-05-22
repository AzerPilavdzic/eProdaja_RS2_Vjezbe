using eProdaja.Authentication;
using eProdaja.Controllers;
using eProdaja.Filters;
using eProdaja.Model.SearchObjects;
using eProdaja.Services;
using eProdaja.Services.Database;
using eProdaja.Services.Interfaces;
using eProdaja.Services.ProductStateMachine;
using eProdaja.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x =>
x.Filters.Add<ErrorFilter>()
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//kako smo ostvarili da se svkia put provjerava autentikacija
builder.Services.AddSwaggerGen(
    c =>
    {
        c.AddSecurityDefinition("basicAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
        {
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "basic"

        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {


            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                { Type = ReferenceType.SecurityScheme, Id = "basicAuth" }
            },
                new string[]{ }
            } });
    });

//implementacija auto mappera
builder.Services.AddAutoMapper(typeof(IKorisniciService));
//sva mapiranja iz Databasea gdje se nalaze sve klase, u nas Model ce se nalaziti u projektu eProdaja.Services
//potrebno je da imamo bilo kakvu klasu ili interfejs referenciranu kroz deklaraciju iznad (AddAutoMapper);


//builder.Services.AddSingleton<IProizvodiService, ProizvodiService>();

builder.Services.AddTransient<IKorisniciService, KorisniciService>();
builder.Services.AddTransient<IJedinicaMjereService, JedinicaMjereService>();

builder.Services.AddTransient<IVrsteProizvodumService, VrsteProizvodumService>();

//za interface IProizvodiService implementacija ce mu biti ProizvodiService a unutar nje se Geta iz baze podataka
builder.Services.AddTransient<IProizvodiService, ProizvodiService>();

builder.Services.AddTransient<IService<eProdaja.Model.Uloge,BaseSearchObject>,BaseService<eProdaja.Model.Uloge, Uloge, BaseSearchObject>>();


//dependency injection za stanja objekta u CRUDu.
builder.Services.AddTransient<BaseState>();
builder.Services.AddTransient<InitialProductState>();
builder.Services.AddTransient<DraftProductState>();
builder.Services.AddTransient<ActiveProductState>();

//ovdje smo dodali liniju kako bi naglasili da ce se raditi o autentikaciji
builder.Services.AddAuthentication("BasicAuthentication")
    //pozivom AddScheme, u T2 naglasimo koja klasa ce se baviti autentikacijom
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

//builder.Services.AddTransient<, >();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<eProdajaContext>(options =>
options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
