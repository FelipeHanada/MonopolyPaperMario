using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyPaperMario.MonopolyGame.Exceptions;
using MonopolyPaperMario.MonopolyGame.Interface;

namespace MonopolyPaperMario.MonopolyGame.Model
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public int Dinheiro { get; private set; }
        public bool Falido { get; private set; }
        public bool Preso { get; private set; }
        public int TurnosPreso { get; private set; }
        public List<IPosseJogador> Posses { get; }

        // Atributos para as cartas e efeitos especiais
        public bool Reverso { get; set; }
        public int Desconto { get; set; }
        public bool PodeComprar { get; set; }
        public Jogador pagador { get; set; }
        public bool TemLifeShroom { get; set; }
        public bool TemBoost { get; set; }
        public int QtdATirarNoProximoTurno { get; set; }
        public bool PodeJogar { get; set; }

        public Jogador(string nome, int dinheiroInicial = 1500)
        {
            Nome = nome;
            Dinheiro = dinheiroInicial;
            Posses = new List<IPosseJogador>();
            Falido = false;
            Preso = false;
            TurnosPreso = 0;

            // Inicialização dos novos atributos
            Reverso = false;
            Desconto = 0;
            PodeComprar = true;
            pagador = this;
            TemLifeShroom = false;
            TemBoost = false;
            QtdATirarNoProximoTurno = 0;
        }

        public void IncrementarTurnosPreso()
        {
            if (Preso)
            {
                TurnosPreso++;
            }
        }

        public void AdicionarPropriedade(IPosseJogador posse)
        {
            if (posse != null)
            {
                Posses.Add(posse);
            }
        }

        public void TransferirPossePara(Jogador destinatario, IPosseJogador posse)
        {
            if (destinatario == null) throw new ArgumentNullException(nameof(destinatario));
            if (posse == null) throw new ArgumentNullException(nameof(posse));
            if (posse.Proprietario != this)
            {
                throw new PosseNaoEDoJogadorCorrenteException(posse, this, "A posse não pertence ao jogador que está tentando transferi-la.");
            }

            if (Posses.Remove(posse))
            {
                destinatario.AdicionarPropriedade(posse);
                posse.Proprietario = destinatario;
            }
        }

        public void TransferirDinheiroPara(Jogador destinatario, int valor)
        {
            if (destinatario == null) throw new ArgumentNullException(nameof(destinatario));
            if (valor == 0) return;

            if (valor > 0) // Ofertante (this) paga ao Alvo (destinatario)
            {
                this.Debitar(valor);
                destinatario.Creditar(valor);
            }
            else // Alvo (destinatario) paga ao Ofertante (this) (valor é negativo)
            {
                destinatario.Debitar(-valor);
                this.Creditar(-valor);
            }
        }

        public void Creditar(int valor)
        {
            if (valor < 0) throw new ArgumentException("O valor a ser creditado não pode ser negativo.");
            Dinheiro += valor;
        }

        public void Debitar(int valor)
        {
            if (valor < 0) throw new ArgumentException("O valor a ser debitado não pode ser negativo.");
            if (Dinheiro < valor)
            {
                // Corrigido: Passa uma string como segundo argumento, não um int.
                throw new FundosInsuficientesException(this, $"Não há fundos suficientes para debitar ${valor}.");
            }
            Dinheiro -= valor;
        }

        public void SetFalido(bool falido)
        {
            this.Falido = falido;
            if (falido)
            {
                Console.WriteLine($"O jogador {Nome} faliu!");
            }
        }

        public void SetPreso(bool preso)
        {
            this.Preso = preso;
            if (!preso)
            {
                this.TurnosPreso = 0;
            }
        }
    }
}