﻿using System;
using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Model;

namespace MonopolyPaperMario.MonopolyGame.Exceptions
{
    public class PosseNaoEDoJogadorCorrenteException : Exception
    {
        public Jogador JogadorTentandoAcao { get; }
        public IPosseJogador Posse { get; }

        public PosseNaoEDoJogadorCorrenteException(IPosseJogador posse, Jogador jogador, string message = "O jogador não é o dono da posse.")
            : base(message)
        {
            this.Posse = posse;
            this.JogadorTentandoAcao = jogador;
        }
    }
}