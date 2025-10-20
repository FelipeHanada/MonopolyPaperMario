using MonpolyMario.Components.Game.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.model
{
    class Partida
    {
        private static Partida partida = null;

        private int casasDisponiveis = 32;
        private int hoteisDisponiveis = 12;
        private int turno = 0;
        private Jogador[] jogadores;
        public static Partida getPartida(Jogador[] jogadores)
        {
            if (partida == null)
            {
                partida = new Partida(jogadores);
            }
            return partida;
        }

        private Partida(Jogador[]jogadores)
        {
            if (jogadores.Length<2||jogadores.Length>6)
            {
                throw new JogadoresDemaisException();
            }
            this.jogadores = jogadores;
        }
        public Jogador getJogadorAtual()
        {
            return this.jogadores[turno];
        }
        public jogador[] getjogadores()
        {
            return this.jogadores;
        }
        public int getTurno()
        {
            return this.turno;
        }
        public int getCasDisponiveis()
        {
            return this.casasDisponiveis;
        }
        public int getHoteisDisponiveis()
        {
            return this.hoteisDisponiveis;
        }
        public void setTurno(int turno)
        {
            if (turno < 0 || turno>=jogadores.Length) // igual também porque o length sempre tem um a mais que o índice
            {
                throw new TurnoInvalidoException();
            }
            this.turno = turno;
        }
        public void setCasasDisponiveis(int casas)
        {
            this.casasDisponiveis = casas;
        }
        public void setHoteisDisponiveis(int hoteis)
        {
            this.hoteisDisponiveis = hoteis;
        }
    }
}