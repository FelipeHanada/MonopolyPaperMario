using MonopolyPaperMario.MonopolyGame.Interface;
using System;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class Piso
    {
        public string Nome { get; }
        // Corrigido: Transformado em uma propriedade pública com setter privado
        public IEfeitoJogador? EfeitoAcao { get; private set; }

        // Construtor para pisos sem efeito especial
        public Piso(string nome)
        {
            this.Nome = nome;
            this.EfeitoAcao = null;
        }

        // Construtor para pisos com um efeito
        public Piso(string nome, IEfeitoJogador efeito)
        {
            this.Nome = nome;
            this.EfeitoAcao = efeito ?? throw new ArgumentNullException(nameof(efeito));
        }

        // Método que ativa o efeito do piso
        public void Efeito(Jogador jogador)
        {
            if (EfeitoAcao != null)
            {
                EfeitoAcao.Execute(jogador);
            }
            else
            {
                Console.WriteLine($"{jogador.Nome} parou em '{Nome}', mas nada acontece.");
            }
        }
    }
}