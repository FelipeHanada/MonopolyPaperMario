using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyPaperMario.MonopolyGame.Impl;
using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class Partida
    {
        public List<Jogador> Jogadores { get; private set; }
        public Tabuleiro? Tabuleiro { get; private set; }
        public int CasasDisponiveis { get; private set; } = 32;
        public int HoteisDisponiveis { get; private set; } = 12;
        public Jogador? JogadorAtual => (jogadorAtualIndex >= 0 && jogadorAtualIndex < Jogadores.Count) ? Jogadores[jogadorAtualIndex] : null;
        private int jogadorAtualIndex;

        public Partida()
        {
            Jogadores = new List<Jogador>();
            jogadorAtualIndex = -1;
        }

        public void AdicionarJogador(string nome)
        {
            if (Jogadores.Count < 6)
            {
                Jogadores.Add(new Jogador(nome));
                Console.WriteLine($"Jogador {nome} adicionado.");
            }
        }

        public void IniciarPartida()
        {
            if (Jogadores.Count < 2) return;

            var pisos = CriarPisos();
            this.Tabuleiro = new Tabuleiro(pisos, Jogadores);
            ((EfeitoIrParaCadeia)pisos[30].EfeitoAcao!).Tabuleiro = this.Tabuleiro;            
            jogadorAtualIndex = -1;
            Console.WriteLine("A partida começou!");
        }

        public void ProximoTurno()
        {
            if (Jogadores.Count(j => !j.Falido) <= 1) return;

            do
            {
                jogadorAtualIndex = (jogadorAtualIndex + 1) % Jogadores.Count;
            } while (Jogadores[jogadorAtualIndex].Falido);
        }

        private (IDeck, IDeck) CriarDecks()
        {
            var cartasCofre = new List<CartaCofre>
            {
                //new CartaCofre("Erro do banco ao seu favor. Receba $200.", new EfeitoPagarReceber(200)),
                //new CartaCofre("Taxa médica. Pague $50.", new EfeitoPagarReceber(-50)),
                //new CartaCofre("Restituição de imposto de renda. Receba $20.", new EfeitoPagarReceber(20)),
                //new CartaCofre("Você herdou $100.", new EfeitoPagarReceber(100))
            };

            var cartasSorte = new List<CartaSorte>
            {
                //new CartaSorte("Seu empréstimo de construção vence. Receba $150.", new EfeitoPagarReceber(150)),
                //new CartaSorte("Pague uma multa por excesso de velocidade de $15.", new EfeitoPagarReceber(-15)),
                //new CartaSorte("Você ganhou um concurso de palavras cruzadas. Receba $100.", new EfeitoPagarReceber(100))
            };

            var deckCofre = new Deck<CartaCofre>(cartasCofre);
            var deckSorte = new Deck<CartaSorte>(cartasSorte);

            return (deckCofre, deckSorte);
        }

        private Piso[] CriarPisos()
        {
            var pisos = new Piso[40];
            var (deckCofre, deckSorte) = CriarDecks();

            // Lado 1
            pisos[0] = new Piso("Ponto de Partida");
            var goombaVillage = new Imovel("Goomba Village", 60, "Marrom", new int[] { 2, 10, 30, 90, 160, 250 }, 50);
            pisos[1] = new Piso(goombaVillage.Nome, new EfeitoPropriedadeCompravel(goombaVillage, this));
            pisos[2] = new Piso("Cofre Comunitário", new EfeitoComprarCarta(deckCofre));
            var koopaVillage = new Imovel("Koopa Village", 60, "Marrom", new int[] { 4, 20, 60, 180, 320, 450 }, 50);
            pisos[3] = new Piso(koopaVillage.Nome, new EfeitoPropriedadeCompravel(koopaVillage, this));
            pisos[4] = new Piso("Imposto de Renda", new EfeitoPagarReceber(-200));
            var toadTownStation = new LinhaTrem("Toad Town Train Station");
            pisos[5] = new Piso(toadTownStation.Nome, new EfeitoPropriedadeCompravel(toadTownStation, this));
            var pleasantPath = new Imovel("Pleasant Path", 100, "Azul Claro", new int[] { 6, 30, 90, 270, 400, 550 }, 50);
            pisos[6] = new Piso(pleasantPath.Nome, new EfeitoPropriedadeCompravel(pleasantPath, this));
            pisos[7] = new Piso("Sorte ou Revés", new EfeitoComprarCarta(deckSorte));
            var foreverForest = new Imovel("Forever Forest", 100, "Azul Claro", new int[] { 6, 30, 90, 270, 400, 550 }, 50);
            pisos[8] = new Piso(foreverForest.Nome, new EfeitoPropriedadeCompravel(foreverForest, this));
            var gustyGulch = new Imovel("Gusty Gulch", 120, "Azul Claro", new int[] { 8, 40, 100, 300, 450, 600 }, 50);
            pisos[9] = new Piso(gustyGulch.Nome, new EfeitoPropriedadeCompravel(gustyGulch, this));

            // Lado 2
            pisos[10] = new Piso("Cadeia (Apenas Visitando)");
            var tubbaCastle = new Imovel("Tubba Bubba's Castle", 140, "Rosa", new int[] { 10, 50, 150, 450, 625, 750 }, 100);
            pisos[11] = new Piso(tubbaCastle.Nome, new EfeitoPropriedadeCompravel(tubbaCastle, this));
            var powerPlant = new Companhia("Power Plant");
            pisos[12] = new Piso(powerPlant.Nome, new EfeitoPropriedadeCompravel(powerPlant, this));
            var boosMansion = new Imovel("Boo's Mansion", 140, "Rosa", new int[] { 10, 50, 150, 450, 625, 750 }, 100);
            pisos[13] = new Piso(boosMansion.Nome, new EfeitoPropriedadeCompravel(boosMansion, this));
            var shyGuyToyBox = new Imovel("Shy Guy's Toy Box", 160, "Rosa", new int[] { 12, 60, 180, 500, 700, 900 }, 100);
            pisos[14] = new Piso(shyGuyToyBox.Nome, new EfeitoPropriedadeCompravel(shyGuyToyBox, this));
            var mtRuggedRailway = new LinhaTrem("Mt. Rugged Railway");
            pisos[15] = new Piso(mtRuggedRailway.Nome, new EfeitoPropriedadeCompravel(mtRuggedRailway, this));
            var dryDryDesert = new Imovel("Dry Dry Desert", 180, "Laranja", new int[] { 14, 70, 200, 550, 750, 950 }, 100);
            pisos[16] = new Piso(dryDryDesert.Nome, new EfeitoPropriedadeCompravel(dryDryDesert, this));
            pisos[17] = new Piso("Cofre Comunitário", new EfeitoComprarCarta(deckCofre));
            var dryDryOutpost = new Imovel("Dry Dry Outpost", 180, "Laranja", new int[] { 14, 70, 200, 550, 750, 950 }, 100);
            pisos[18] = new Piso(dryDryOutpost.Nome, new EfeitoPropriedadeCompravel(dryDryOutpost, this));
            var dryDryRuins = new Imovel("Dry Dry Ruins", 200, "Laranja", new int[] { 16, 80, 220, 600, 800, 1000 }, 100);
            pisos[19] = new Piso(dryDryRuins.Nome, new EfeitoPropriedadeCompravel(dryDryRuins, this));

            // Lado 3
            pisos[20] = new Piso("Parada Livre");
            var flowerFields = new Imovel("Flower Fields", 220, "Vermelho", new int[] { 18, 90, 250, 700, 875, 1050 }, 150);
            pisos[21] = new Piso(flowerFields.Nome, new EfeitoPropriedadeCompravel(flowerFields, this));
            pisos[22] = new Piso("Sorte ou Revés", new EfeitoComprarCarta(deckSorte));
            var flowerGardens = new Imovel("Flower Gardens", 220, "Vermelho", new int[] { 18, 90, 250, 700, 875, 1050 }, 150);
            pisos[23] = new Piso(flowerGardens.Nome, new EfeitoPropriedadeCompravel(flowerGardens, this));
            var crystalPalace = new Imovel("Crystal Palace", 240, "Vermelho", new int[] { 20, 100, 300, 750, 925, 1100 }, 150);
            pisos[24] = new Piso(crystalPalace.Nome, new EfeitoPropriedadeCompravel(crystalPalace, this));
            var starshipStation = new LinhaTrem("Starship Station");
            pisos[25] = new Piso(starshipStation.Nome, new EfeitoPropriedadeCompravel(starshipStation, this));
            var yoshisVillage = new Imovel("Yoshi's Village", 260, "Amarelo", new int[] { 22, 110, 330, 800, 975, 1150 }, 150);
            pisos[26] = new Piso(yoshisVillage.Nome, new EfeitoPropriedadeCompravel(yoshisVillage, this));
            var jadeJungle = new Imovel("Jade Jungle", 260, "Amarelo", new int[] { 22, 110, 330, 800, 975, 1150 }, 150);
            pisos[27] = new Piso(jadeJungle.Nome, new EfeitoPropriedadeCompravel(jadeJungle, this));
            var waterWorks = new Companhia("Water Works");
            pisos[28] = new Piso(waterWorks.Nome, new EfeitoPropriedadeCompravel(waterWorks, this));
            var lavalavaLand = new Imovel("Lavalava Land", 280, "Amarelo", new int[] { 24, 120, 360, 850, 1025, 1200 }, 150);
            pisos[29] = new Piso(lavalavaLand.Nome, new EfeitoPropriedadeCompravel(lavalavaLand, this));

            // Lado 4
            pisos[30] = new Piso("Vá para a Cadeia", new EfeitoIrParaCadeia());
            var shiverCity = new Imovel("Shiver City", 300, "Verde", new int[] { 26, 130, 390, 900, 1100, 1275 }, 200);
            pisos[31] = new Piso(shiverCity.Nome, new EfeitoPropriedadeCompravel(shiverCity, this));
            var shiverMountain = new Imovel("Shiver Mountain", 300, "Verde", new int[] { 26, 130, 390, 900, 1100, 1275 }, 200);
            pisos[32] = new Piso(shiverMountain.Nome, new EfeitoPropriedadeCompravel(shiverMountain, this));
            pisos[33] = new Piso("Cofre Comunitário", new EfeitoComprarCarta(deckCofre));
            var crystalKingsPalace = new Imovel("Crystal King's Palace", 320, "Verde", new int[] { 28, 150, 450, 1000, 1200, 1400 }, 200);
            pisos[34] = new Piso(crystalKingsPalace.Nome, new EfeitoPropriedadeCompravel(crystalKingsPalace, this));
            var bowsersFleet = new LinhaTrem("Bowser's Fleet");
            pisos[35] = new Piso(bowsersFleet.Nome, new EfeitoPropriedadeCompravel(bowsersFleet, this));
            pisos[36] = new Piso("Sorte ou Revés", new EfeitoComprarCarta(deckSorte));
            var starbornValley = new Imovel("Starborn Valley", 350, "Roxo", new int[] { 35, 175, 500, 1100, 1300, 1500 }, 200);
            pisos[37] = new Piso(starbornValley.Nome, new EfeitoPropriedadeCompravel(starbornValley, this));
            pisos[38] = new Piso("Imposto de Luxo", new EfeitoPagarReceber(-100));
            var bowsersCastle = new Imovel("Bowser's Castle", 400, "Roxo", new int[] { 50, 200, 600, 1400, 1700, 2000 }, 200);
            pisos[39] = new Piso(bowsersCastle.Nome, new EfeitoPropriedadeCompravel(bowsersCastle, this));

            return pisos;
        }
    }
}