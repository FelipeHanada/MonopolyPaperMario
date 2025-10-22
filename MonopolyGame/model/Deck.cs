using MonopolyPaperMario.MonopolyGame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    // Adicionada a implementação da interface IDeck
    public class Deck<T> : IDeck where T : class, ICarta
    {
        private List<T> cartas;
        private Random rng = new Random();

        public Deck(List<T> cartasIniciais)
        {
            this.cartas = cartasIniciais ?? new List<T>();
            Embaralhar();
        }

        public void Embaralhar()
        {
            cartas = cartas.OrderBy(c => rng.Next()).ToList();
        }

        // Implementação do método da interface
        ICarta? IDeck.ComprarCarta()
        {
            return this.ComprarCartaGenerico();
        }

        public T? ComprarCartaGenerico()
        {
            if (cartas.Count == 0)
            {
                Console.WriteLine("O deck está vazio!");
                return null;
            }

            T carta = cartas[0];
            cartas.RemoveAt(0);
            return carta;
        }
    }
}