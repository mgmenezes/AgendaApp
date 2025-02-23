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
using AgendaApp.Application.MessageBus.Interfaces;
using MediatR;
using AgendaApp.Application.Commands.Contatos.CriarContato;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AgendaApp.Infrastructure.Repositories;

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<IValidator<CriarContatoDto>, CriarContatoValidator>();
builder.Services.AddSingleton<IMessageBusService, RabbitMQService>();
builder.Services.AddHostedService<ContatoConsumer>();
// builder.Services.AddScoped<IValidator<AtualizarContatoDto>, AtualizarContatoValidator>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CriarContatoCommand).Assembly));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();



var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();