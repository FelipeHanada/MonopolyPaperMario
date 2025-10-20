using MonpolyMario.Components.Game.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonpolyMario.Components.Game.Model
{
    public class Jogador
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
            if (this.dinheiro<valor)
            {
                throw new FundosInsuficientesException(this);
            }
            this.dinheiro -= valor;
            jogador.setDinheiro(jogador.getDinheiro()+valor);
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
        public PosseJogador[] getPosses()
        {
            return this.posses;
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
