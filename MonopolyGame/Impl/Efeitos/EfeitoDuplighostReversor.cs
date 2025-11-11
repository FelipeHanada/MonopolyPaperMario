using MonopolyGame.Utils;
using MonopolyGame.Interface.Efeitos;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Efeitos;


// Este efeito é o que é AGENDADO. 
// Ele será setado no jogador e removido no fim do turno.
public class EfeitoDuplighostReversor(Jogador duplighostAtivo) : IEfeitoJogador
{
    public Jogador DuplighostAlvo { get; } = duplighostAtivo;
    private bool foiUsado = false; 

    // Este método é chamado na Partida.ProximoTurno quando o contador zera/expira.
    public void Aplicar(Jogador jogador) 
    {
        // Limpeza: remove a referência do efeito no jogador quando ele expira
        jogador.EfeitoDuplighostAtivo = null;
    }

    // NOVO MÉTODO: Chamado pelo EfeitoPropriedadeCompravel para executar a lógica
    public bool UsarEfeito(Propriedade propriedade)
    {
        if (foiUsado || DuplighostAlvo.Falido)
        {
            return false;
        }

        // Regra principal: O Duplighost só paga se NÃO for o proprietário
        if (propriedade.Proprietario != DuplighostAlvo)
        {
            foiUsado = true;
            return true; // Use o Duplighost!
        }
        else
        {
            // Regra extra: Duplighost é o proprietário. Ele se transformou nele mesmo.
            Log.WriteLine($"[Duplighost] O Duplighost ({DuplighostAlvo.Nome}) é o proprietário. O efeito falhou.");
            DuplighostAlvo.Partida.AdicionarRegistro($"[Duplighost] O Duplighost ({DuplighostAlvo.Nome}) é o proprietário. O efeito falhou.");
            // O efeito falha, mas DEVE ser consumido, ou o jogador acionador usará no próximo turno.
            foiUsado = true;
            return false; // Não use, pague normalmente.
        }
    }
}
