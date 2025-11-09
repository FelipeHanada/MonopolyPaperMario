using MonopolyGame.Interface.Controles;
using MonopolyGame.Impl.Controles;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using MonopolyGame.Interface.PosseJogador;
using MonopolyGame.Model.PropostasTroca;
using MonopolyGame.Interface.Partidas;


namespace MonopolyGameTest
{
    [TestClass]
    public sealed class TestControle
    {
        [TestMethod]
        public void TestRolarDados()
        {
            Partida partida = new(["J1", "J2", "J3", "J4"]);
            IControlePartida controle = new ControlePartida(partida);

            Assert.AreEqual(EstadoTurnoId.Comum, partida.EstadoTurnoAtual.EstadoId);
            Assert.AreEqual(partida.Jogadores[0], controle.Partida.JogadorAtual);
            controle.Comum_RolarDados();
            Assert.AreEqual(partida.Jogadores[0], controle.Partida.JogadorAtual);

        }

        [TestMethod]
        public void TestPropostaTroca()
        {
            Partida partida = new(["J1", "J2", "J3", "J4"]);
            IControlePartida controle = new ControlePartida(partida);

            IPosseJogador a = new Companhia("a");
            IPosseJogador b = new Companhia("b");

            partida.Jogadores[0].AdicionarPosse(a);
            partida.Jogadores[1].AdicionarPosse(b); 

            PropostaTroca propostaTroca = new PropostaTroca(partida.JogadorAtual, partida.Jogadores[1]);
            propostaTroca.PossesOfertadas.Add(a);
            propostaTroca.PossesDesejadas.Add(b);
            propostaTroca.DinheiroOfertado = 100;

            controle.Troca_Iniciar(propostaTroca);

            Assert.AreEqual(EstadoTurnoId.PropostaTroca, partida.EstadoTurnoAtual.EstadoId);

            Assert.AreEqual(propostaTroca, partida.EstadoTurnoAtual.PropostaTroca);

            controle.Troca_Aceitar();

            Assert.AreEqual(1, partida.Jogadores[0].Posses.Count);
            Assert.AreEqual(1, partida.Jogadores[1].Posses.Count);

            Assert.IsNotNull(partida.Jogadores[0].Posses.First(posse => posse == b));
            Assert.IsNotNull(partida.Jogadores[1].Posses.First(posse => posse == a));
            Assert.AreEqual(1400, partida.Jogadores[0].Dinheiro);
            Assert.AreEqual(1600, partida.Jogadores[1].Dinheiro);
        }
    }
}
