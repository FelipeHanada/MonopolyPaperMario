using System;
using System.Linq;
using MonopolyPaperMario.MonopolyGame.Impl;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class TurnoJogador
    {
        private static TurnoJogador? _instance;
        private readonly Random dado;
        private Partida? partidaAtual;
        private Jogador? jogadorDaVez;
        private int contadorDadosIguais;

        private TurnoJogador()
        {
            dado = new Random();
        }

        public static TurnoJogador Instance
        {
            get
            {
                _instance ??= new TurnoJogador();
                return _instance;
            }
        }

        public void IniciarTurno(Partida partida)
        {
            this.partidaAtual = partida;
            this.jogadorDaVez = partida.JogadorAtual;
            this.contadorDadosIguais = 0;

            if (jogadorDaVez == null) return;

            Console.WriteLine($"\n--- É a vez de {jogadorDaVez.Nome} ---");
            Console.WriteLine($"Saldo: ${jogadorDaVez.Dinheiro} | Posição: {partida.Tabuleiro?.GetPosicao(jogadorDaVez)}");

            if (jogadorDaVez.Preso)
            {
                TentarSairDaPrisao();
            }
            else
            {
                RolarDados();
            }
        }

        private void TentarSairDaPrisao()
        {
            if (jogadorDaVez == null || partidaAtual == null || partidaAtual.Tabuleiro == null) return;

            jogadorDaVez.IncrementarTurnosPreso();
            Console.WriteLine($"{jogadorDaVez.Nome} está na prisão. (Turno {jogadorDaVez.TurnosPreso})");

            // No terceiro turno, força a saída pagando fiança
            if (jogadorDaVez.TurnosPreso >= 3)
            {
                Console.WriteLine("Terceiro turno na prisão! Você deve pagar $50 para sair.");
                try
                {
                    jogadorDaVez.Debitar(50);
                    Console.WriteLine("Fiança paga.");
                    new EfeitoSairDaCadeia().Execute(jogadorDaVez);
                    RolarDados(); // Agora que está livre, joga normalmente
                }
                catch (Exceptions.FundosInsuficientesException)
                {
                    Console.WriteLine("Você não tem dinheiro para pagar a fiança e faliu!");
                    jogadorDaVez.SetFalido(true);
                    FinalizarTurno();
                }
                return;
            }

            // Tenta rolar dados iguais nos turnos 1 e 2
            Console.WriteLine("Tentando rolar dados iguais para sair...");
            Console.WriteLine("Pressione Enter para rolar os dados...");
            Console.ReadLine();

            int resultadoDado1 = dado.Next(1, 7);
            int resultadoDado2 = dado.Next(1, 7);
            Console.WriteLine($"Você rolou {resultadoDado1} e {resultadoDado2}.");

            if (resultadoDado1 == resultadoDado2)
            {
                Console.WriteLine("Dados iguais! Você está livre!");
                new EfeitoSairDaCadeia().Execute(jogadorDaVez);

                int totalDados = resultadoDado1 + resultadoDado2;
                partidaAtual.Tabuleiro.MoveJogador(jogadorDaVez, totalDados);
            }
            else
            {
                Console.WriteLine("Você não rolou dados iguais e continua na prisão.");
            }

            FinalizarTurno();
        }

        private void RolarDados()
        {
            if (jogadorDaVez == null || partidaAtual == null || partidaAtual.Tabuleiro == null) return;

            bool jogarNovamente = true;
            while (jogarNovamente)
            {
                Console.WriteLine("\nPressione Enter para rolar os dados...");
                Console.ReadLine();

                int resultadoDado1 = dado.Next(1, 7);
                int resultadoDado2 = dado.Next(1, 7);
                int totalDados = resultadoDado1 + resultadoDado2;
                bool dadosIguais = resultadoDado1 == resultadoDado2;

                Console.WriteLine($"{jogadorDaVez.Nome} rolou os dados e tirou {resultadoDado1} e {resultadoDado2}, totalizando {totalDados}.");

                if (dadosIguais)
                {
                    contadorDadosIguais++;
                    Console.WriteLine("Dados iguais!");

                    if (contadorDadosIguais == 3)
                    {
                        Console.WriteLine("Três pares de dados iguais seguidos! Vá para a cadeia!");
                        var efeitoCadeia = new EfeitoIrParaCadeia { Tabuleiro = partidaAtual.Tabuleiro };
                        efeitoCadeia.Execute(jogadorDaVez);
                        jogarNovamente = false;
                    }
                    else
                    {
                        partidaAtual.Tabuleiro.MoveJogador(jogadorDaVez, totalDados);
                        Console.WriteLine("Jogue novamente!");
                        jogarNovamente = true;
                    }
                }
                else
                {
                    partidaAtual.Tabuleiro.MoveJogador(jogadorDaVez, totalDados);
                    jogarNovamente = false;
                }
            }
            FinalizarTurno();
        }

        public void IniciarLeilao(Propriedade propriedade)
        {
            if (partidaAtual == null) return;
            Console.WriteLine($"--- Leilão para {propriedade.Nome} ---");
            var leilao = new Leilao(propriedade, partidaAtual.Jogadores.Where(j => !j.Falido).ToList(), jogadorDaVez);
            leilao.Executar();
            FinalizarLeilao(leilao);
        }

        private void FinalizarLeilao(Leilao leilao)
        {
            Console.WriteLine("Leilão finalizado.");
        }

        public void ComecarPropostaDeVenda(PropostaDeVenda proposta)
        {
            Console.WriteLine($"Proposta de venda iniciada: {proposta.Vendedor.Nome} -> {proposta.Comprador.Nome} para {proposta.Posse.Nome}");
            FinalizarPropostaDeVenda(proposta, true);
        }

        private void FinalizarPropostaDeVenda(PropostaDeVenda proposta, bool aceita)
        {
            Console.WriteLine($"Proposta de venda finalizada. Aceita: {aceita}");
        }

        private void FinalizarTurno()
        {
            Console.WriteLine($"Fim do turno de {jogadorDaVez?.Nome}.");
        }
    }
}