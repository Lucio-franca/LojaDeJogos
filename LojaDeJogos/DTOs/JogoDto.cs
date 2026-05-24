using System.ComponentModel.DataAnnotations;

namespace LojaDeJogos.DTOs;

public class JogoDto
{
    [Required(ErrorMessage = "Título obrigatório.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Gênero obrigatório.")]
    public string Genero { get; set; } = string.Empty;

    [Required(ErrorMessage = "Plataforma obrigatória.")]
    public string Plataforma { get; set; } = string.Empty;

    [Range(0.01, 9999.99, ErrorMessage = "Preço inválido.")]
    public decimal Preco { get; set; }

    [Range(0, 10000, ErrorMessage = "Estoque inválido.")]
    public int Estoque { get; set; }

    [Required(ErrorMessage = "Desenvolvedora obrigatória.")]
    public string Desenvolvedora { get; set; } = string.Empty;

    [Range(1970, 2100, ErrorMessage = "Ano inválido.")]
    public int AnoLancamento { get; set; }
}