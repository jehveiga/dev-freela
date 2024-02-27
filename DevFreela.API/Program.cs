using DevFreela.API.Filters;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Payments;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DI
var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Adicionando servi�o do Jwt na aplica��o
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddHttpClient();

// Adicionar o servi�o de Filter para valida��o no Pipeline da requisi��o usando o options do servi�o da AddControllers
builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));

// Fluent Validation, adicionar� todas dependecias de servi�o pela classe obtendo o Assembly da classe informada no parametro
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

// Adicionando o servi�o do MediatR no container de servi�o do Asp.Net, adicionar� todas dependecias de servi�o pelo Assembly informado no parametro
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(CreateProjectCommand).Assembly); });



builder.Services.AddEndpointsApiExplorer();

// Adicionando as configura��es da documenta��o no Swagger das difinic�es de seguran�a
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });

    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });
});

// Adicionando o servi�o de autentica��o no container de servi�os com as configura��es para gera��o do token e valida��o
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
                      IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                  };
              });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFreela.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
