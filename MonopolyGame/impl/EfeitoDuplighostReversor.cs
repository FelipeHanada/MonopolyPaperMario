using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using System;

namespace MonopolyPaperMario.MonopolyGame.Impl
{
    // Este efeito é o que é AGENDADO. 
    // Ele será setado no jogador e removido no fim do turno.
    public class EfeitoDuplighostReversor : IEfeitoJogador
    {
        public Jogador DuplighostAlvo { get; }
        
        // Flag para garantir que o efeito só seja usado uma vez
        public bool FoiUsado { get; private set; } = false; 

        public EfeitoDuplighostReversor(Jogador duplighostAlvo)
        {
            this.DuplighostAlvo = duplighostAlvo ?? throw new ArgumentNullException(nameof(duplighostAlvo));
        }

        // Este método é chamado na Partida.ProximoTurno quando o contador zera/expira.
        public void Execute(Jogador jogador) 
        {
            // Limpeza: remove a referência do efeito no jogador quando ele expira
            jogador.EfeitoDuplighostAtivo = null;
            Console.WriteLine($"[Duplighost] O efeito Duplighost expirou para {jogador.Nome}.");
        }

        // NOVO MÉTODO: Chamado pelo EfeitoPropriedadeCompravel para executar a lógica
        public bool UsarEfeito(Propriedade propriedade)
        {
            if (FoiUsado || DuplighostAlvo.Falido)
            {
                return false;
            }
            
            // Regra principal: O Duplighost só paga se NÃO for o proprietário
            if (propriedade.Proprietario != DuplighostAlvo)
            {
                FoiUsado = true;
                return true; // Use o Duplighost!
            }
            else
            {
                // Regra extra: Duplighost é o proprietário. Ele se transformou nele mesmo.
                Console.WriteLine($"[Duplighost] O Duplighost ({DuplighostAlvo.Nome}) é o proprietário. O efeito falhou.");
                // O efeito falha, mas DEVE ser consumido, ou o jogador acionador usará no próximo turno.
                FoiUsado = true;
                return false; // Não use, pague normalmente.
            }
        }
    }
}