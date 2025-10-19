using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using MonopolyPaperMario.Interface; // Assumindo que ICarta e IEfeitoCarta estão aqui
using MonopolyPaperMario.model;    // Para CartaSorte, CartaCofre e outras classes

namespace MonopolyPaperMario.model
{
    public class Deck
    {
        // 1. Coleções internas para armazenar as cartas
        // O diagrama mostra um relacionamento 1 com 0..* Carta, representando os decks.
        private List<ICarta> deckCofre;
        private List<ICarta> deckSorte;
        
        // Fila de cartas pegas para reembaralhar
        private Queue<ICarta> cartasPegasCofre;
        private Queue<ICarta> cartasPegasSorte;

        // Propriedade para gerar números aleatórios (para embaralhar e pegar cartas)
        private readonly Random random = new Random();

        public Deck()
        {
            // Inicializa as coleções e carrega os dados
            deckCofre = new List<ICarta>();
            deckSorte = new List<ICarta>();
            cartasPegasCofre = new Queue<ICarta>();
            cartasPegasSorte = new Queue<ICarta>();
            
            CarregarCartas();
        }

        // ... (Implementação dos métodos de Carregamento JSON abaixo) ...
        
        // 2. Método público exigido pelo diagrama: pegarCarta()
        // O método deve retornar uma 'Carta' (ou 'ICarta') e precisa saber qual deck usar.
        
        public ICarta pegarCartaCofre()
        {
            return PegarCarta(deckCofre, cartasPegasCofre);
        }

        public ICarta pegarCartaSorte()
        {
            return PegarCarta(deckSorte, cartasPegasSorte);
        }
        
        // Método auxiliar para pegar e gerenciar as cartas
        private ICarta PegarCarta(List<ICarta> deckPrincipal, Queue<ICarta> cartasPegas)
        {
            if (deckPrincipal.Count == 0)
            {
                // Reembaralha as cartas pegas e as move de volta para o deck principal
                Reembaralhar(deckPrincipal, cartasPegas);
                
                if (deckPrincipal.Count == 0)
                {
                    throw new InvalidOperationException("Ambos os decks principal e de cartas pegas estão vazios.");
                }
            }
            
            // Pega a primeira carta (ou a última, dependendo de como você implementou o embaralhamento)
            ICarta cartaPega = deckPrincipal[0];
            deckPrincipal.RemoveAt(0);
            
            // Adiciona a carta na fila de cartas que precisam ser reembaralhadas
            cartasPegas.Enqueue(cartaPega);
            
            return cartaPega;
        }

        private void Reembaralhar(List<ICarta> deckPrincipal, Queue<ICarta> cartasPegas)
        {
            Console.WriteLine("Reembaralhando as cartas...");
            
            // Move todas as cartas da fila para uma lista temporária
            List<ICarta> tempDeck = cartasPegas.ToList();
            cartasPegas.Clear();
            
            // Embaralha (Fisher-Yates shuffle simplificado)
            int n = tempDeck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                ICarta value = tempDeck[k];
                tempDeck[k] = tempDeck[n];
                tempDeck[n] = value;
            }
            
            // Move as cartas embaralhadas de volta para o deck principal
            deckPrincipal.AddRange(tempDeck);
        }

       private string ObterCaminhoDoArquivo(string nomeArquivo){
        // Lógica para encontrar o caminho do arquivo JSON dentro da pasta Assets
        // Esta implementação assume que a pasta 'Assets' está na raiz do binário de execução.
        var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        // Ajuste o caminho conforme a estrutura do seu projeto se necessário (ex: voltando 3 pastas)
        var caminhoAssets = Path.Combine(assemblyLocation, "Assets"); 
    
        return Path.Combine(caminhoAssets, nomeArquivo);
    }

    private void CarregarCartas(){
        // Carregar Cartas de Cofre
        var caminhoCofre = ObterCaminhoDoArquivo("CartasDeCofre.json");
        CarregarCartasCofre(caminhoCofre);

        // Carregar Cartas de Sorte
        var caminhoSorte = ObterCaminhoDoArquivo("CartasDeSorte.json");
        CarregarCartasSorte(caminhoSorte);
    
        // Embaralha os decks iniciais para garantir que a primeira carta não seja sempre a mesma
        Reembaralhar(deckCofre, cartasPegasCofre);
        Reembaralhar(deckSorte, cartasPegasSorte);
    }


    private void CarregarCartasCofre(string caminhoArquivo)
    {
        try
        {
            string jsonString = File.ReadAllText(caminhoArquivo);
            // Desserializa diretamente para a classe CartaCofre (assumindo propriedades públicas)
            var cartasData = JsonSerializer.Deserialize<List<CartaCofre>>(jsonString);

            foreach (var carta in cartasData)
            {
                // Adiciona as cartas ao deck
                deckCofre.Add(carta);
            }
            Console.WriteLine($"[Deck] Carregadas {deckCofre.Count} cartas de cofre.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Deck ERRO] Falha ao carregar Cartas de Cofre: {ex.Message}");
        }
    }

    private void CarregarCartasSorte(string caminhoArquivo)
    {
        try
        {
            string jsonString = File.ReadAllText(caminhoArquivo);
        // Desserializa diretamente para a classe CartaSorte (assumindo propriedades públicas)
            var cartasData = JsonSerializer.Deserialize<List<CartaSorte>>(jsonString);

            foreach (var data in cartasData)
            {
            // Usa a Factory para injetar a lógica de efeito (como discutido anteriormente)
                IEfeitoCarta efeitoLogica = EfeitoCartaFactory.CriarEfeito(data);
                data.SetEfeitoLogica(efeitoLogica); // Atribui a lógica de estratégia
            
                deckSorte.Add(data);
            }
            Console.WriteLine($"[Deck] Carregadas {deckSorte.Count} cartas de sorte.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Deck ERRO] Falha ao carregar Cartas de Sorte: {ex.Message}");
        }
        }
    }
}