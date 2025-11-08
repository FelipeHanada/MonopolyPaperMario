using MonopolyGame.Model.Leiloes;
using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.PossesJogador;
using System.Runtime.ConstrainedExecution;

namespace MonopolyGameTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Partida partida = new(["J1", "J2", "J3", "J4"]);

            partida.IniciarLeilao(partida.JogadorAtual, new Imovel("leiloada", 100, "v", [1, 2, 3, 4, 5, 6], 50, 25));

            partida.EstadoTurnoAtual.DarLanceLeilao(50);
            Assert.AreEqual(50, partida.EstadoTurnoAtual.Leilao.MaiorLance);
            Assert.AreEqual(partida.Jogadores[0], partida.EstadoTurnoAtual.Leilao.MaiorLicitante);
            Assert.AreEqual(partida.Jogadores[1], partida.EstadoTurnoAtual.JogadorAtualLeilao);

            partida.EstadoTurnoAtual.DarLanceLeilao(50);
            Assert.AreEqual(100, partida.EstadoTurnoAtual.Leilao.MaiorLance);
            Assert.AreEqual(partida.Jogadores[1], partida.EstadoTurnoAtual.Leilao.MaiorLicitante);
            Assert.AreEqual(partida.Jogadores[2], partida.EstadoTurnoAtual.JogadorAtualLeilao);

            partida.EstadoTurnoAtual.DarLanceLeilao(50);
            Assert.AreEqual(150, partida.EstadoTurnoAtual.Leilao.MaiorLance);
            Assert.AreEqual(partida.Jogadores[2], partida.EstadoTurnoAtual.Leilao.MaiorLicitante);
            Assert.AreEqual(partida.Jogadores[3], partida.EstadoTurnoAtual.JogadorAtualLeilao);

            partida.EstadoTurnoAtual.DarLanceLeilao(50);
            Assert.AreEqual(200, partida.EstadoTurnoAtual.Leilao.MaiorLance);
            Assert.AreEqual(partida.Jogadores[3], partida.EstadoTurnoAtual.Leilao.MaiorLicitante);
            Assert.AreEqual(partida.Jogadores[0], partida.EstadoTurnoAtual.JogadorAtualLeilao);

            partida.EstadoTurnoAtual.DesistirLeilao();
            Assert.AreEqual(200, partida.EstadoTurnoAtual.Leilao.MaiorLance);
            Assert.AreEqual(partida.Jogadores[3], partida.EstadoTurnoAtual.Leilao.MaiorLicitante);

            partida.EstadoTurnoAtual.DesistirLeilao();
            Assert.AreEqual(200, partida.EstadoTurnoAtual.Leilao.MaiorLance);
            Assert.AreEqual(partida.Jogadores[3], partida.EstadoTurnoAtual.Leilao.MaiorLicitante);

            partida.EstadoTurnoAtual.DesistirLeilao();
            Assert.AreEqual(200, partida.EstadoTurnoAtual.Leilao.MaiorLance);
            Assert.AreEqual(partida.Jogadores[3], partida.EstadoTurnoAtual.Leilao.MaiorLicitante);

            Assert.IsTrue(partida.EstadoTurnoAtual.Leilao.Finalizado);

            partida.EstadoTurnoAtual.DarLanceLeilao(50);
            Assert.AreEqual(200, partida.EstadoTurnoAtual.Leilao.MaiorLance);
            Assert.AreEqual(partida.Jogadores[3], partida.EstadoTurnoAtual.Leilao.MaiorLicitante);

            partida.EncerrarLeilao();

            Assert.AreEqual(0, partida.Jogadores[0].Posses.Count);
            Assert.AreEqual(0, partida.Jogadores[1].Posses.Count);
            Assert.AreEqual(0, partida.Jogadores[2].Posses.Count);
            Assert.AreEqual(1, partida.Jogadores[3].Posses.Count);
        }
    }
}
