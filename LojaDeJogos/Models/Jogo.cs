namespace LojaDeJogos.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Plataforma { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Desenvolvedora { get; set; } = string.Empty;
        public int AnoLancamento { get; set; }
        public DateTime CadastradoEm { get; set; } = DateTime.Now;
    }
}
