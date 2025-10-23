using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;
using MonopolyGame.impl; // Para acessar os efeitos

namespace MonopolyGame.impl.Cartas
{
    internal class CartaMuskular : CartaSorte
    {
        public CartaMuskular() : base(
            "Muskular ativou seu poder Chill Out. Durante 3 turnos todas as propriedades e despesas terão 30% de desconto.", 
            // Usa o Efeito Aplicar Desconto genérico com 30%
            new EfeitoAplicarDesconto(30)) 
        {
            
        }
        public override void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"Sorte: {Descricao}");
            
            // 1. EXECUTA a aplicação do efeito (Desconto = 30)
            Efeito?.Execute(jogador); 
            
            Console.WriteLine("Agendando reversão do efeito Muskular Chill Out para 3 turnos.");
            
            // 2. AGENDA a reversão para daqui a 3 turnos usando o EfeitoReverterDesconto
            Partida.GetPartida().addEfeitoTurnoParaJogadores(
                3, // O efeito dura 3 turnos
                new EfeitoReverterDesconto(), 
                [jogador]
            );
        }
    }
}