using LojaDeJogos.DTOs;
using LojaDeJogos.Interfaces;
using LojaDeJogos.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeJogos.Controllers;

[ApiController]
[Route("api/loja-de-jogos")]
public class JogosController : ControllerBase
{
    private static readonly List<Jogo> _jogos = new()
    {
        new Jogo { Id = 1, Titulo = "The Last of Us Part I", Genero = "Aventura", Plataforma = "PlayStation 5", Preco = 299.90m, Estoque = 15, Desenvolvedora = "Naughty Dog", AnoLancamento = 2022 },
        new Jogo { Id = 2, Titulo = "Elden Ring", Genero = "RPG", Plataforma = "PC", Preco = 249.90m, Estoque = 30, Desenvolvedora = "FromSoftware", AnoLancamento = 2022 },
        new Jogo { Id = 3, Titulo = "EA Sports FC 25", Genero = "Esporte", Plataforma = "PlayStation 5", Preco = 349.90m, Estoque = 50, Desenvolvedora = "EA Sports", AnoLancamento = 2024 },
    };

    private static int _proximoId = 4;

    private readonly IValidadorJogo _validador;
    private readonly ILogger<JogosController> _logger;

    public JogosController(IValidadorJogo validador, ILogger<JogosController> logger)
    {
        _validador = validador;
        _logger = logger;
    }

    [HttpGet("lista-jogos")]
    public ActionResult<List<Jogo>> ListarJogos()
    {
        _validador.Log("Listagem solicitada.");
        return Ok(_jogos);
    }

    [HttpGet("buscar-jogo/{id:int}")]
    public ActionResult<Jogo> BuscarJogo(int id)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == id);
        if (jogo is null)
            return NotFound(new { Mensagem = $"Jogo com Id {id} não encontrado." });
        return Ok(jogo);
    }

    [HttpPost("cadastrar-jogo")]
    public async Task<IActionResult> CadastrarJogo([FromBody] JogoDto dto)
    {
        if (!ModelState.IsValid)
        {
            var errosModel = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(new { Mensagem = "Dados inválidos.", Erros = errosModel });
        }

        var errosNegocio = _validador.Validar(dto);
        if (errosNegocio.Any())
            return BadRequest(new { Mensagem = "Erro nas regras de negócio.", Erros = errosNegocio });

        // Simula persistência no banco — não bloqueia a thread
        await Task.Delay(2000);

        var novoJogo = new Jogo
        {
            Id = _proximoId++,
            Titulo = dto.Titulo,
            Genero = dto.Genero,
            Plataforma = dto.Plataforma,
            Preco = dto.Preco,
            Estoque = dto.Estoque,
            Desenvolvedora = dto.Desenvolvedora,
            AnoLancamento = dto.AnoLancamento,
            CadastradoEm = DateTime.Now
        };

        _jogos.Add(novoJogo);
        return CreatedAtAction(nameof(BuscarJogo), new { id = novoJogo.Id }, novoJogo);
    }

    [HttpDelete("remover-jogo/{id:int}")]
    public IActionResult RemoverJogo(int id)
    {
        var jogo = _jogos.FirstOrDefault(j => j.Id == id);
        if (jogo is null)
            return NotFound(new { Mensagem = $"Jogo com Id {id} não encontrado." });
        _jogos.Remove(jogo);
        return Ok(new { Mensagem = $"'{jogo.Titulo}' removido com sucesso." });
    }
}