using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MonopolyPaperMario.Interface; 
using MonopolyPaperMario.model;    

namespace MonopolyPaperMario.model
{
    public class Deck
    {
        // 1. Coleções internas para armazenar as cartas
        private List<ICarta> deckCofre;
        private private List<ICarta> deckSorte;
        
        // Fila de cartas pegas para reembaralhar
        private Queue<ICarta> cartasPegasCofre;
        private Queue<ICarta> cartasPegasSorte;

        // Propriedade para gerar números aleatórios (para embaralhar e pegar cartas)
        private readonly Random random = new Random();

        public Deck()
        {
            // Inicializa as coleções
            deckCofre = new List<ICarta>();
            deckSorte = new List<ICarta>();
            cartasPegasCofre = new Queue<ICarta>();
            cartasPegasSorte = new Queue<ICarta>();
            
            CarregarCartas(); // Chama o método que foi modificado
        }
        
        // 2. Método público exigido pelo diagrama: pegarCarta()
        public ICarta pegarCartaCofre()
        {
            return PegarCarta(deckCofre, cartasPegasCofre);
        }

        public ICarta pegarCartaSorte()
        {
            return PegarCarta(deckSorte, cartasPegasSorte);
        }
        
        // Método auxiliar para pegar e gerenciar as cartas (Lógica inalterada)
        private ICarta PegarCarta(List<ICarta> deckPrincipal, Queue<ICarta> cartasPegas)
        {
            if (deckPrincipal.Count == 0)
            {
                Reembaralhar(deckPrincipal, cartasPegas);
                
                if (deckPrincipal.Count == 0)
                {
                    throw new InvalidOperationException("Ambos os decks principal e de cartas pegas estão vazios.");
                }
            }
            
            ICarta cartaPega = deckPrincipal[0];
            deckPrincipal.RemoveAt(0);
            cartasPegas.Enqueue(cartaPega);
            
            return cartaPega;
        }

        // Método de Reembaralhamento (Lógica inalterada)
        private void Reembaralhar(List<ICarta> deckPrincipal, Queue<ICarta> cartasPegas)
        {
            Console.WriteLine("Reembaralhando as cartas...");
            
            List<ICarta> tempDeck = cartasPegas.ToList();
            cartasPegas.Clear();
            
            int n = tempDeck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                ICarta value = tempDeck[k];
                tempDeck[k] = tempDeck[n];
                tempDeck[n] = value;
            }
            
            deckPrincipal.AddRange(tempDeck);
        }

        // --- NOVO MÉTODO DE CARREGAMENTO NO DECK ---
        // A lógica do caminho do arquivo e a desserialização foram MOVIDAS
        // para CartaInstantanea.CarregarDoJson<T>()
        private void CarregarCartas()
        {
            // 1. Carregar Cartas de Cofre
            // Chama o método estático em CartaInstantanea.
            List<CartaCofre> cartasCofre = CartaInstantanea.CarregarDoJson<CartaCofre>("CartasDeCofre.json");

            foreach (var carta in cartasCofre)
            {
                deckCofre.Add(carta);
            }
            
            // 2. Carregar Cartas de Sorte
            // Chama o método estático em CartaInstantanea.
            List<CartaSorte> cartasSorte = CartaInstantanea.CarregarDoJson<CartaSorte>("CartasDeSorte.json");

            foreach (var data in cartasSorte)
            {
                // **A lógica de injeção de efeito (Factory) permanece aqui**
                // porque é o Deck quem está montando o objeto CartaSorte completo
                // para o jogo.
                IEfeitoCarta efeitoLogica = EfeitoCartaFactory.CriarEfeito(data);
                data.SetEfeitoLogica(efeitoLogica); 
                
                deckSorte.Add(data);
            }
            
            // 3. Embaralha os decks iniciais
            Reembaralhar(deckCofre, cartasPegasCofre);
            Reembaralhar(deckSorte, cartasPegasSorte);
            
            Console.WriteLine($"[Deck] Carregadas {deckCofre.Count} cartas de cofre.");
            Console.WriteLine($"[Deck] Carregadas {deckSorte.Count} cartas de sorte.");
        }
    }
}