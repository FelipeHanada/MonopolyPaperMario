using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyPaperMario.model
{
    abstract class Propriedade:PosseJogador
    {
        private bool hipotecado;
        private int valorHipoteca;

        public bool isHipotecado()
        {
            return hipotecado;
        }
        public int getValorHipoteca()
        {
            return valorHipoteca;
        }
        public void setHipotecado(bool hipotecado)
        {
            this.hipotecado = hipotecado;
        }
        public void setValorHipoteca(int hipoteca)
        {
            this.valorHipoteca = hipoteca;
        }
        public abstract int calculaPagamento();

    }
}
