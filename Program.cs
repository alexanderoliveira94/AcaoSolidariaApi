using Microsoft.EntityFrameworkCore;
using AcaoSolidariaApi.Data;
using AcaoSolidariaApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicione serviços ao contêiner.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoLocal"));
});

// Configurando a injeção de dependência para as interfaces e implementações
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IOngService, OngService>();
builder.Services.AddScoped<IPublicacaoService, PublicacaoService>();

builder.Services.AddCors();

builder.Services.AddControllers();

// Adicione a autenticação por cookies
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Cookie";
    options.DefaultSignInScheme = "Cookie";
    options.DefaultChallengeScheme = "Cookie";
})
.AddCookie("Cookie", options =>
{
    options.Cookie.Name = "MyCookie";
    options.LoginPath = "/Account/Login"; // Substitua pelo caminho da sua página de login
});

// Configure o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redireciona para HTTPS (descomente se necessário)
// app.UseHttpsRedirection();

app.UseRouting();

// Adiciona autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
