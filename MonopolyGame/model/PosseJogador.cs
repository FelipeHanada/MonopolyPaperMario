using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.model
{
    abstract class PosseJogador
    {
        private Jogador dono;
        private String nome;

        public String getNome() { 
            return nome;
        }
        public Jogador getDono()
        {
            return dono;
        }
        public void setDono(Jogador dono)
        {
            this.dono = dono;
        }
        public void setNome(String nome)
        {
            this.nome = nome;
        }
    }
}
