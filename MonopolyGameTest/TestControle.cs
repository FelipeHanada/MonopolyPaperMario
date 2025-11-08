using MonopolyGame.Interface.Controles;
using MonopolyGame.Impl.Controles;
using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using System.Runtime.ConstrainedExecution;
using MonopolyGame.Interface.Partidas;

namespace MonopolyGameTest
{
    [TestClass]
    public sealed class TestControle
    {
        [TestMethod]
        public void TestMethod1()
        {
            Partida partida = new(["J1", "J2", "J3", "J4"]);
            IControlePartida controle = new ControlePartida(partida);

            Assert.AreEqual(partida.Jogadores[0], controle.Partida.JogadorAtual);
            controle.Comum_RolarDados();
            Assert.AreEqual(partida.Jogadores[0], controle.Partida.JogadorAtual);
        }
    }
}
