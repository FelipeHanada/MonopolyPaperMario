using MonopolyPaperMario.MonopolyGame.Interface;
using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class CartaPoder : ICarta
    {
        public string NomePoder { get; private set; }

        public CartaPoder(string nomePoder)
        {
            NomePoder = nomePoder;
        }

        public void QuandoPegada(Jogador jogador)
        {
            Console.WriteLine($"{jogador.Nome} adquiriu o poder: {NomePoder}!");
            // Lógica para adicionar o poder ao jogador
        }
    }
}