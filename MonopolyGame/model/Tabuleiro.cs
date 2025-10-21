
namespace MonopolyPaperMario.model
{
    class Tabuleiro
    {
        private List<PosicaoJogador> posicaoJogadores;

        private Piso[] pisos;

        public Tabuleiro(Piso[] pisos, List<PosicaoJogador> posicoesIniciais)
        {
            // Validação básica para garantir 40 pisos
            if (pisos == null || pisos.Length != 40)
            {
                throw new ArgumentException("O Tabuleiro deve ser inicializado com um array de 40 Pisos.");
            }
            
            this.pisos = pisos;
            this.posicaoJogadores = posicoesIniciais;
        }

        public void moveJogador(Jogador jogador, int offset, bool efeitoPiso)
        {
            PosicaoJogador posAtual = this._posicaoJogadores.FirstOrDefault(p => p.getJogador() == jogador);

            if (posAtual == null) return; // não tem jogador

            int posAnterior = posAtual.getPosition;

            int novaPosicao = (posAnterior + offset) % this._pisos.Length;


            // ver como alterar caso tenha o efeito do groove guy
            if (novaPosicao < posAnterior)
            {
                // O jogador deu uma volta completa no tabuleiro.
                Console.WriteLine($"{jogador.getNome()} passou pelo INÍCIO e recebeu $200!");

                jogador.setDinheiro(jogador.getDinheiro() + 200);
            }

            posAtual.setPosicao(novaPosicao);

            // o que fazer em caso do jogador parar em alguma casa que tenha efeito ?
            // como casa de sorte ou pagar propriedade ou ir para a cadeia ?



        }
        
        public void moveJogadorPara(Jogador jogador, int piso, bool efeitoPiso)
        {
            
            // Validação do índice do piso
            if (piso < 0 || piso >= this._pisos.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(piso), "O índice do piso deve estar entre 0 e 39.");
            }
            
            // 1. Encontra a PosiçãoAtual do jogador
            PosicaoJogador posAtual = this._posicaoJogadores.FirstOrDefault(p => p.getJogador() == jogador);

            if (posAtual == null) return;

            // 2. Move o Jogador
            posAtual.setPosicao(piso);

            
            // O que fazer no caso de efeito de piso ?

        }

    }
}