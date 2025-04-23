using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderSolution.API.Context;
using OrderSolution.API.Filters;
using OrderSolution.API.Secutiry.Token;
using OrderSolutions.Exception;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Insira o token JWT no formato: Bearer {seu_token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            new string[] {}
        }
    });
});

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                       ?? builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddDbContext<OrderSolutionDbContext>(c =>
{
    c.UseSqlServer(connectionString);
});

builder.Services.AddMvc(opt => opt.Filters.Add<ExceptionFilter>());

builder.Services.AddHttpContextAccessor();

var tokenGenerator = new SymetricGenerator();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = tokenGenerator.GetCredentials()
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();