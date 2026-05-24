using LojaDeJogos.Interfaces;
using LojaDeJogos.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de Dependência — Scoped: uma instância por requisição
builder.Services.AddScoped<IValidadorJogo, ValidadorJogo>();

// CORS — permite o front-end acessar a API
builder.Services.AddCors(o =>
    o.AddPolicy("PermitirTudo", p =>
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("PermitirTudo");
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();