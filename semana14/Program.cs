using Microsoft.AspNetCore.Identity;
using semana12;
using semana12.Endpoints;
using semana12.Infra;
using semana12.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Security.Claims;


var root = AppDomain.CurrentDomain.BaseDirectory;
var dotenv = Path.Combine(root, ".env");
var dotEnv = new DotEnv();
dotEnv.CarregarCaminho(dotenv);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AtletaContext>();
builder.Services.AddSingleton<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

builder.Services.AddCors();


builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Config.Instancia.ChavePrivada)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = ctx =>
        {
            ctx.Request.Cookies.TryGetValue("acessToken", out var acessToken);
            if(!string.IsNullOrEmpty(acessToken))
                ctx.Token = acessToken;
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin", x => x.RequireRole("admin"))
    .AddPolicy("comum", x => x.RequireRole("comum"))
    .AddPolicy("adminOuComum", policy => policy.RequireClaim(ClaimTypes.Role, "admin", "comum"));

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AdicionarAtletasEndpoint();
app.AdicionarUsuarioEndpoint();
app.AdicionarLoginEndpoint();

app.Run();
