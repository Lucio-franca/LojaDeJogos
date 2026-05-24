using LojaDeJogos.DTOs;

namespace LojaDeJogos.Interfaces;

public interface IValidadorJogo
{
    List<string> Validar(JogoDto dto);
    void Log(string mensagem);
}