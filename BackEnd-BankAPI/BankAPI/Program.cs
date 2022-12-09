using BankAPI.Data;
using BankAPI.Services;
using BankAPI.Services.Contrato;
using BankAPI.Services.Implementacion;
using BankAPI.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// conectamos la BD - DbContext
builder.Services.AddSqlServer<BankAPIContext>(builder.Configuration.GetConnectionString("BankConnection"));

// minusculas URL
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

// Injectamos servicios (Service Layer)
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountTypeService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

// inyectamos el automapper creado
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Configuracion para los Cors de Angular
builder.Services.AddCors(options => {
    options.AddPolicy("NuevaPolitica", app => {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Injectamos JWT Bearer que instalamos con Nuget
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdmin", policy => policy.RequireClaim("AdminType", "Super"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Agregamos la autentication antes del Authorization, para JWT
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// injectamos los cors, que configuramos
app.UseCors("NuevaPolitica");

app.Run();
