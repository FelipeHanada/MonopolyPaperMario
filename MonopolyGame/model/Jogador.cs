﻿using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyPaperMario.MonopolyGame.Exceptions;
using MonopolyPaperMario.MonopolyGame.Interface;
using MonopolyPaperMario.MonopolyGame.Impl;

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

        // NOVO: Contador de cartas de Passe Livre da Prisão
        public int CartasPasseLivre { get; set; }
        public EfeitoDuplighostReversor? EfeitoDuplighostAtivo { get; set; } // Guarda a referência do efeito
        // Atributos para as cartas e efeitos especiais
        public bool Reverso { get; set; }
        public int Desconto { get; set; }
        public bool PodeComprar { get; set; }
        public Jogador pagador { get; set; }
        public bool TemBoost { get; set; }
        public int QtdATirarNoProximoTurno { get; set; }
        public bool PodeJogar { get; set; }
        public int Multiplicador { get; set; }

        public Jogador(string nome, int dinheiroInicial = 1500)
        {
            Nome = nome;
            Dinheiro = dinheiroInicial;
            Posses = new List<IPosseJogador>();
            Falido = false;
            Preso = false;
            TurnosPreso = 0;
            CartasPasseLivre = 0;

            // Inicialização dos novos atributos
            Reverso = false;
            Desconto = 0;
            PodeComprar = true;
            PodeJogar = true;
            pagador = this;
            TemBoost = false;
            EfeitoDuplighostAtivo = null;
            QtdATirarNoProximoTurno = 0;
            Multiplicador = 0;
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
            valor = AplicarDesconto(valor);
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

        public int AplicarDesconto(int valorBase)
        {
            if (this.Desconto <= 0)
            {
                return valorBase; // Sem desconto, retorna o valor original
            }
            Console.WriteLine("Oba, " + this.Nome + " teve um desconto no débito.");
        // Calcula o fator de desconto (ex: 30 / 100 = 0.3)
            double fatorDesconto = (double)this.Desconto / 100.0;
    
    // Calcula o valor final: Valor Base * (1 - Fator de Desconto)
    // Usamos Math.Round para garantir que o resultado seja um inteiro (arredondando para o mais próximo).
            int valorFinal = (int)Math.Round(valorBase * (1.0 - fatorDesconto));
    
    // Mensagem de log para facilitar o debug e o feedback ao usuário
            int valorDescontado = valorBase - valorFinal;
            Console.WriteLine($"[Muskular] Despesa de ${valorBase} ajustada para ${valorFinal} (-${valorDescontado} de desconto).");
    
            return valorFinal;
        }

    }
}