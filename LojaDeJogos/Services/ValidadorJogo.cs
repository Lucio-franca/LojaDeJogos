using LojaDeJogos.DTOs;
using LojaDeJogos.Interfaces;

namespace LojaDeJogos.Services;

public class ValidadorJogo : IValidadorJogo
{
    private readonly ILogger<ValidadorJogo> _logger;

    private static readonly HashSet<string> Generos = new(StringComparer.OrdinalIgnoreCase)
        { "Ação","Aventura","RPG","Esporte","Corrida","Estratégia","Simulação","Terror","Puzzle","Luta" };

    private static readonly HashSet<string> Plataformas = new(StringComparer.OrdinalIgnoreCase)
        { "PC","PlayStation 5","PlayStation 4","Xbox Series X","Xbox One","Nintendo Switch","Mobile" };

    public ValidadorJogo(ILogger<ValidadorJogo> logger) => _logger = logger;

    public List<string> Validar(JogoDto dto)
    {
        var erros = new List<string>();

        if (dto.Titulo.All(char.IsDigit))
            erros.Add("O título não pode conter apenas números.");

        if (!Generos.Contains(dto.Genero))
            erros.Add($"Gênero inválido. Aceitos: {string.Join(", ", Generos)}.");

        if (!Plataformas.Contains(dto.Plataforma))
            erros.Add($"Plataforma inválida. Aceitas: {string.Join(", ", Plataformas)}.");

        if (dto.Preco == 0 && dto.Estoque > 0)
            erros.Add("Jogo com estoque deve ter preço maior que zero.");

        if (dto.AnoLancamento > DateTime.Now.Year + 1)
            erros.Add("Ano de lançamento não pode ultrapassar o próximo ano.");

        Log(erros.Count == 0
            ? $"'{dto.Titulo}' aprovado."
            : $"'{dto.Titulo}' reprovado com {erros.Count} erro(s).");

        return erros;
    }

    public void Log(string mensagem) =>
        _logger.LogInformation("[ValidadorJogo] {Msg} | {Hora}", mensagem, DateTime.Now);
}