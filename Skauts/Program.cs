using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Skauts.Data.Context;
using Skauts.Profiles;
using Skauts.Services;
using Skauts.Services.Implementations;
using Skauts.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serviços Core do ASP.NET Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar o Swagger com Bearer Token Authentication
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Skauts API",
        Version = "v1",
        Description = "API para gerenciamento de dados do Skauts"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira 'Bearer' [espaço] e então seu token no campo abaixo.\n\nExemplo: 'Bearer 12345abcdef'"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configurar o DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SkautsDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Configurar o AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configurar Política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSkautsApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Injeção de Dependência dos Serviços da Aplicação

// Serviços Principais
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IChampionshipService, ChampionshipService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();

// Serviços de "Lookup" (Apoio)
builder.Services.AddScoped<IEventTypeService, EventTypeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPrizeTypeService, PrizeTypeService>();
builder.Services.AddScoped<IPlayersPrizeService, PlayersPrizeService>();

// Serviços de Relação (N:N)
builder.Services.AddScoped<IUsersOrganizationService, UsersOrganizationService>();
builder.Services.AddScoped<ITeamPlayerService, TeamPlayerService>();

// Serviços Core

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Permite que serviços "Scoped" (como o DbContext) acessem o HttpContext
builder.Services.AddHttpContextAccessor();

// Adiciona o serviço de autorização
builder.Services.AddAuthorization();

// --- Construção do App ---
var app = builder.Build();

// Bloco para aplicar migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<SkautsDbContext>();
        dbContext.Database.Migrate();
        Console.WriteLine("Migrations aplicadas com sucesso.");
    }
    catch (Exception ex)
    {
        // Log de erro
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations.");
    }
}

// Configurar o Pipeline de Middlewares

// Usar Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skauts API V1");
    });
}

app.UseHttpsRedirection();

// Aplica a política de CORS definida
app.UseCors("AllowSkautsApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();