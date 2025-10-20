using MonpolyMario.Components.Game.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.model
{
    class Jogador
    {
        public Jogador(String nome)
        {
            this.nome = nome;
        }
        private bool falido;
        private bool preso;
        private String nome;
        private int dinheiro;
        private PosseJogador[] posses;

        public void addPosse(PosseJogador posse)
        {
            posses.Append(posse);
        }
        public void transferirPossePara(Jogador jogador, PosseJogador posse)
        {
            if (!posse.getDono().Equals(this)) // se a posse não é do jogador corrente
            {
                throw new PosseNaoEDoJogadorCorrenteException(jogador, posse);
            }
            
            PosseJogador[] newPossesCurrent = new PosseJogador[this.posses.Length-1]; //cria um novo array de posses com tamanho diminuído em 1
            int i = 0;
            foreach (PosseJogador p in posses)
            {
                if (!(p.Equals(posse))) // se a posse do array for diferente da que eu vou transferir
                {
                    newPossesCurrent[i++] = p; // adiciono no novo array de posses e incremento o índice
                }
            }
            this.posses = newPossesCurrent;
            jogador.addPosse(posse);
            
        }
        public void transferirDinheiroPara(Jogador jogador, int valor)
        {
            if (this.dinheiro < valor)
            {
                throw new FundosInsuficientesException(this);
            }
            this.dinheiro -= valor;
            jogador.setDinheiro(jogador.getDinheiro() + valor);
        }
        
        public void mudarDinheiro(int valor)
        {
        // Se o valor for positivo, o jogador recebe dinheiro.
        // Se o valor for negativo, o jogador paga dinheiro.
    
        // Verifica se a operação é um PAGAMENTO (valor negativo)
            if (valor < 0) 
            {
                int valorAbsoluto = Math.Abs(valor);
        
            // Verifica se há fundos suficientes antes de pagar
                if (this.dinheiro < valorAbsoluto)
                {
            // Você pode lançar uma exceção ou lidar com a falência aqui.
            // Vou usar a mesma lógica que o seu transferirDinheiroPara faria.
                    throw new FundosInsuficientesException(this);
                }
            }
    
        // Se for RECEBIMENTO (valor positivo) ou PAGAMENTO com fundos OK,
        // o dinheiro é alterado.
            this.dinheiro += valor;
        }
        
        public void setFalido(bool falido)
        {
            this.falido = falido;
        }
        public void setPreso(bool preso)
        {
            this.preso = preso;
        }
        public void setDinheiro(int dinheiro)
        {
            this.dinheiro = dinheiro;
        }
        public bool isFalido()
        {
            return falido;
        }
        public String getNome()
        {
            return nome;
        }
        public int getDinheiro()
        {
            return dinheiro;
        }
        public bool isPreso()
        {
            return preso;
        }
    }
}
