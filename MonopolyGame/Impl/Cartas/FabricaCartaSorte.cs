using MonopolyGame.Model.Partidas;
using MonopolyGame.Model.Cartas;
using MonopolyGame.Interface.Cartas;
using MonopolyGame.Impl.Efeitos;

namespace MonopolyGame.Impl.Cartas;


class FabricaCartaSorte(Partida partida) : FabricaAbstrataCartaSorte
{
    private readonly Partida partida = partida;

    public CartaSorte CriaCarta(CartasSorte cartaId)
    {
        CartaSorte? carta = cartaId switch
        {
            CartasSorte.CartaBowserShuffle => new CartaSorte(
                "Bowser ativou a sua Star Rod e fez com que todos os jogadores em campo tenham o mesmo dinheiro.",
                new EfeitoBowserShuffle(partida)
            ),
            CartasSorte.CartaBlooper => new CartaSorte(
                "Blooper jogou tinta em você e você não pode ver nada. Fique 1 rodada sem comprar qualquer propriedade ou companhia.",
                new EfeitoAgendarEfeito(partida, new EfeitoReverterComprarJogador(partida), 1)
            ),
            CartasSorte.CartaDuplighost => new CartaSorte(
                "Duplighost apareceu! Ele se transformará em um jogador aleatório e pagará sua próxima despesa de propriedade/aluguel.",
                new EfeitoDuplighost(partida)
            ),
            CartasSorte.CartaGrooveGuyTonto => new CartaSorte(
                "Groove Guy te deixou tonto. Você vai se mover na direção contrária no próximo turno.",
                new EfeitoComposto(partida, [
                    new EfeitoReverterDirecaoJogador(partida),
                    new EfeitoAgendarEfeito(partida, new EfeitoReverterDirecaoJogador(partida), 1)
                ])
            ),
            CartasSorte.CartaLavaVulcao => new CartaSorte(
                "Você se queimou na lava do vulcão, volte 3 casas.",
                new EfeitoMoverJogador(partida, -3)
            ),
            CartasSorte.CartaLuteComBowser => new CartaSorte(
                "Oh não, o Bowser está aqui, lute contra ele e ajude o reino dos cogumelos.",
                new EfeitoIrParaCadeia(partida)
            ),
            CartasSorte.CartaMagikoopaAmarelo => new CartaSorte(
                "Yellow Magikoopa ativou sua mágica e agora todos os jogadores no próximo turno lhe pagarão 100 moedas",
                new EfeitoAgendarEfeito(partida, new EfeitoMagikoopaAmarelo(partida), 1)
            ),
            CartasSorte.CartaMagikoopaVermelho => new CartaSorte(
                "Red Magikoopa te deu um boost e durante o próximo turno a quantidade de casas a mover será duplicada.",
                new EfeitoComposto(partida, [
                    new EfeitoMagikoopaVermelho(partida),
                    new EfeitoAgendarEfeito(partida, new EfeitoMagikoopaVermelho(partida), 1),
                ])
            ),
            CartasSorte.CartaMartelo => new CartaSorte(
                "Você encontrou o martelo e quebrou o bloco que bloqueava a passagem para Toad Town, avance 3 casas",
                new EfeitoMoverJogador(partida, 3)
            ),
            CartasSorte.CartaMuskular => new CartaSorte(
                "Muskular ativou seu poder Chill Out. Durante 3 turnos todas as propriedades e despesas terão 30% de desconto.",
                new EfeitoComposto(partida, [
                    new EfeitoAplicarDesconto(partida,30),
                    new EfeitoAgendarEfeito(partida, new EfeitoReverterDesconto(partida), 3)
                ])
            ),
            CartasSorte.CartaPeDeFeijao => new CartaSorte(
                "Você plantou o pé de feijão que te levou até as nuvens. Ao sair, você retornou para Toad Town (pare na partida e ganhe 200)",
                new EfeitoMoverJogadorPara(partida, 0)
            ),
            CartasSorte.CartaSentinels => new CartaSorte(
                "Os Sentinels pegaram todos os jogadores e os trocaram de lugar uns com os outros.",
                new EfeitoRotacionarPosicao(partida)
            ),
            CartasSorte.CartaSpinyTromp => new CartaSorte(
                "Oh não, um Spiny Tromp. Fuja dele e avance 4 casas",
                new EfeitoMoverJogador(partida, 4)
            ),
            CartasSorte.CartaStarBeam => new CartaSorte(
                "Passe Livre da Prisão. Esta carta pode ser guardada até que seja necessária ou vendida.",
                new EfeitoDarPasseLivre(partida, 1)
            ),
            CartasSorte.CartaTimeout => new CartaSorte(
                "Kevlar ativou seu poder timeout. Você ficará uma rodada sem jogar.",
                new EfeitoComposto(partida, [
                    new EfeitoReverterPodeJogar(partida),
                    new EfeitoAgendarEfeito(partida, new EfeitoReverterPodeJogar(partida), 1)
                ])
            ),
            CartasSorte.CartaTrocaCano => new CartaSorte(
                "Você encontrou uma passagem em um cano. Troque de lugar com outro jogador.",
                new EfeitoTrocaPosicaoRandomico(partida)
            ),
            _ => throw new ArgumentException($"ID de carta desconhecido: {cartaId}", nameof(cartaId))
        };

        return carta;
    }

    public List<CartaSorte> CriaTodasAsCartas()
    {
        return [
            CriaCarta(CartasSorte.CartaBowserShuffle),
            CriaCarta(CartasSorte.CartaBlooper),
            CriaCarta(CartasSorte.CartaDuplighost),
            CriaCarta(CartasSorte.CartaGrooveGuyTonto),
            CriaCarta(CartasSorte.CartaLavaVulcao),
            CriaCarta(CartasSorte.CartaLuteComBowser),
            CriaCarta(CartasSorte.CartaMagikoopaAmarelo),
            CriaCarta(CartasSorte.CartaMagikoopaVermelho),
            CriaCarta(CartasSorte.CartaMartelo),
            CriaCarta(CartasSorte.CartaMuskular),
            CriaCarta(CartasSorte.CartaPeDeFeijao),
            CriaCarta(CartasSorte.CartaSentinels),
            CriaCarta(CartasSorte.CartaSpinyTromp),
            CriaCarta(CartasSorte.CartaStarBeam),
            CriaCarta(CartasSorte.CartaTimeout),
            CriaCarta(CartasSorte.CartaTrocaCano),
        ];
    }
}
