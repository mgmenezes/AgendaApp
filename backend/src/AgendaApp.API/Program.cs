// AgendaApp.API/Program.cs
using Microsoft.EntityFrameworkCore;
using AgendaApp.Infrastructure.Context;
using AgendaApp.Domain.Interfaces;
using AgendaApp.Infrastructure;
using AgendaApp.Application.Interfaces;
using AgendaApp.Application.Services;
using AgendaApp.Application.DTOs;
using AgendaApp.Application.Validators;
using FluentValidation;
using AgendaApp.API.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Configuração básica dos serviços
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do DbContext
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração do AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Registro dos serviços
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<IValidator<CriarContatoDto>, CriarContatoValidator>();
// builder.Services.AddScoped<IValidator<AtualizarContatoDto>, AtualizarContatoValidator>();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();