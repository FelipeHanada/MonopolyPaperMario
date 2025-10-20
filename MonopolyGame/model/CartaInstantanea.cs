using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using MonopolyPaperMario.Interface;

namespace MonopolyPaperMario.model
{
    public abstract class CartaInstantanea : ICarta
    {
        // Método abstrato forçado para as classes filhas
        public abstract void QuandoPegada(Jogador jogador);

        // NOVO MÉTODO: Responsável por carregar qualquer lista de cartas instantâneas de um arquivo JSON.
        // É estático porque não depende de nenhuma instância de CartaInstantanea.
        public static List<T> CarregarDoJson<T>(string nomeArquivo) where T : CartaInstantanea
        {
            try
            {
                // 1. Lógica para encontrar o caminho do arquivo (pode ser movida para cá)
                var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var caminhoAssets = Path.Combine(assemblyLocation, "Assets");
                var caminhoCompleto = Path.Combine(caminhoAssets, nomeArquivo);

                // 2. Leitura e Desserialização
                string jsonString = File.ReadAllText(caminhoCompleto);
                
                // Desserializa para uma lista do tipo específico (T, que será CartaCofre ou CartaSorte)
                var cartasData = JsonSerializer.Deserialize<List<T>>(jsonString);
                
                return cartasData ?? new List<T>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"[ERRO] Arquivo de cartas não encontrado: {nomeArquivo}.");
                return new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Falha ao carregar {nomeArquivo}: {ex.Message}");
                return new List<T>();
            }
        }
    }
}