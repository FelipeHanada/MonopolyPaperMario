using System;
using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyPaperMario.MonopolyGame.Impl.Efeitos
{

    /// Efeito imediato ativado pelo Magikoopa Vermelho.
    /// Define um multiplicador de 1.5 para a próxima jogada de dados do jogador.

    public class EfeitoMagikoopaVermelho : IEfeitoJogador
    {
        public string Nome => "Mágica do Magikoopa Vermelho (Multiplicador)";
        public string Descricao => "Multiplica o resultado da próxima jogada de dados por 1.5.";

        public void Execute(Jogador jogadorAlvo)
        {
            Console.WriteLine($"--- Efeito Magikoopa Vermelho ativado para {jogadorAlvo.Nome} ---");
            
            const double Multiplicador = 1.5;

            try
            {
                // Usando Reflection (o método do código anterior) para acessar a propriedade 'MultiplicadorDado'.
                // Este código SÓ FUNCIONARÁ se a classe Jogador for atualizada.
                var jogadorType = typeof(Jogador);
                var prop = jogadorType.GetProperty("MultiplicadorDado");
                
                if (prop != null && prop.PropertyType == typeof(double))
                {
                    // Define o multiplicador de dados do jogador para 1.5
                    prop.SetValue(jogadorAlvo, Multiplicador);
                    Console.WriteLine($"[Aplicado] Multiplicador de dados de {Multiplicador:F1}x aplicado com sucesso para {jogadorAlvo.Nome}.");
                }
                else
                {
                    Console.WriteLine("ERRO DE IMPLEMENTAÇÃO: A propriedade 'public double MultiplicadorDado { get; set; }' deve ser adicionada à classe Jogador para que este efeito funcione.");
                }

            }
            catch (Exception ex)
            {
                 Console.WriteLine($"ERRO ao aplicar o efeito Magikoopa Vermelho: Não foi possível aplicar o multiplicador. Detalhe: {ex.Message}");
            }
            
            Console.WriteLine($"--- Fim do Efeito ---");
        }
    }
}