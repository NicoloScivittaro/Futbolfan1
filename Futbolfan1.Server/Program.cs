using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using FutbolFan1.Data;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi questa riga per assicurarti che appsettings.json sia caricato
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllersWithViews();

// Configura la connessione al database
builder.Services.AddDbContext<FutbolFanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FutbolFanDatabase")));

// Configura Identity con le impostazioni di sicurezza avanzate
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;  // Definisci la lunghezza minima della password
})
.AddEntityFrameworkStores<FutbolFanContext>()
.AddDefaultTokenProviders();

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("https://localhost:4200") // Assicurati che sia HTTPS
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Aggiungi questo se usi autenticazione
    });
});

// Configura l'autenticazione JWT
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];
var key = builder.Configuration["Jwt:Key"];

if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(key))
{
    throw new ArgumentNullException("JWT configuration values cannot be null.");
}

var signingKey = Encoding.UTF8.GetBytes(key); // Genera la chiave con almeno 32 caratteri

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(signingKey),  // Usa una chiave sufficientemente lunga
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        ClockSkew = TimeSpan.Zero
    };
});

// Aggiungi i servizi al contenitore
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Configura Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "FutbolFan1 API", Version = "v1" });
});

var app = builder.Build();

// Configura la pipeline delle richieste HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FutbolFan1 API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configura CORS
app.UseCors("AllowAngularApp");

// Configura l'uso di autenticazione e autorizzazione
app.UseAuthentication();  // Deve essere prima di UseAuthorization
app.UseAuthorization();

// Configura le rotte dei controller
app.MapControllers();

// Configura la rotta predefinita per la vista "Home"
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
