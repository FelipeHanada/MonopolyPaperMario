using MonopolyGame.Model.Cartas;
using MonopolyGame.Interface.Cartas;
using MonopolyGame.Impl.Efeitos;
using MonopolyGame.Model.Partidas;

namespace MonopolyGame.Impl.Cartas;


class FabricaCartaCofre(Partida partida) : FabricaAbstrataPropriedade
{
    private const int VALOR_DEBITO_ANTIGUYS = 50;
    private const int VALOR_CREDITO_CHUCKQUIZMO = 50;
    private const int VALOR_DEBITO_KETCHNKOOPA = 100;
    private const int VALOR_CREDITO_KOOPAKOOT = 100;
    private const int VALOR_CREDITO_MERLEE = 80;
    private const int VALOR_CREDITO_MERLUVELEE = 80;
    private const int VALOR_DEBITO_MISTAKE = 80;
    private const int VALOR_CREDITO_MISTAR = 200;
    private const int VALOR_CREDITO_PLAYGROUND = 100;
    private const int VALOR_DEBITO_RALPH = 150;
    private const int VALOR_DEBITO_RIPCHEATO = 60;
    private const int VALOR_DEBITO_SHOPPING = 100;
    private const int VALOR_CREDITO_WHACKA = 65;
    private const int VALOR_CREDITO_YOSHIKIDS = 120;

    private readonly Partida partida = partida;

    public CartaCofre CriaCarta(CartasCofre cartaId)
    {
        CartaCofre carta = cartaId switch
        {
            CartasCofre.CartaAntiGuys => new CartaCofre(
                $"Oh não, você errou 3 questões no castelo do bowser. Ele chamou o esquadrão anti-guy e você gastou todos os itens para derrotá-los. Pague ${VALOR_DEBITO_ANTIGUYS}.",
                new EfeitoDebitoFixo(VALOR_DEBITO_ANTIGUYS)
            ),
            CartasCofre.CartaBandits => new CartaCofre($"Bandits roubaram parte das suas moedas. Pague $60.", new EfeitoDebitoFixo(60)),
            CartasCofre.CartaChetRippo => new CartaCofre(
                $"Seus stats estão desbalanceados e você pediu para chet rippo balancea-los. Pague $65.",
                new EfeitoDebitoFixo(65)
            ),
            CartasCofre.CartaChuckQuizmo => new CartaCofre(
                $"Chuck Quizmo: Você acertou a questão! Recebe ${VALOR_CREDITO_CHUCKQUIZMO}.",
                new EfeitoCreditoFixo(VALOR_CREDITO_CHUCKQUIZMO)
            ),
            CartasCofre.CartaKetchnkoopa => new CartaCofre(
                $"Ketch'n Koopa está bloqueando a sua passagem e você o subornou. Pague ${VALOR_DEBITO_KETCHNKOOPA}.",
                new EfeitoDebitoFixo(VALOR_DEBITO_KETCHNKOOPA)
            ),
            CartasCofre.CartaKoopaKoot => new CartaCofre(
                $"Você ajudou o koopa koot e ganhou acesso ao playground exclusivo! Recebe ${VALOR_CREDITO_KOOPAKOOT}.",
                new EfeitoCreditoFixo(VALOR_CREDITO_KOOPAKOOT)
            ),
            CartasCofre.CartaMerlee => new CartaCofre(
                $"A mágica da merlee foi ativada e você recebeu mais dinheiro que o previsto! Recebe ${VALOR_CREDITO_MERLEE}.",
                new EfeitoCreditoFixo(VALOR_CREDITO_MERLEE)
            ),
            CartasCofre.CartaMerluvelee => new CartaCofre(
                $"Você consultou merluvelee e encontrou todas as star pieces do jogo parabéns! Recebe ${VALOR_CREDITO_MERLUVELEE}.",
                new EfeitoCreditoFixo(VALOR_CREDITO_MERLUVELEE)
            ),
            CartasCofre.CartaMistake => new CartaCofre(
                $"Você misturou na cozinha da Tayce.T um ultra shroom com uma stone cap e saiu um mistake. Pague ${VALOR_DEBITO_MISTAKE}.",
                new EfeitoDebitoFixo(VALOR_DEBITO_MISTAKE)
            ),
            CartasCofre.CartaMistar => new CartaCofre(
                $"Você salvou a mistar e com a ajuda dela você conseguiu vencer os inimigos Recebe ${VALOR_CREDITO_MISTAR}.",
                new EfeitoCreditoFixo(VALOR_CREDITO_MISTAR)
            ),
            CartasCofre.CartaPlayground => new CartaCofre(
                $"Você ganhou o jogo no playground Recebe ${VALOR_CREDITO_PLAYGROUND}.",
                new EfeitoCreditoFixo(VALOR_CREDITO_PLAYGROUND)
            ),
            CartasCofre.CartaRalph => new CartaCofre(
                $"Você foi na loja de badges do ralph's shop e gastou demais. Pague ${VALOR_DEBITO_RALPH}.",
                new EfeitoDebitoFixo(VALOR_DEBITO_RALPH)
            ),
            CartasCofre.CartaRipCheato => new CartaCofre(
                $"Você comprou um dried shroom com o rip cheato. Pague ${VALOR_DEBITO_RIPCHEATO}.",
                new EfeitoDebitoFixo(VALOR_DEBITO_RIPCHEATO)
            ),
            CartasCofre.CartaShopping => new CartaCofre(
                $" você está com hp baixo e precisou ir no shopping comprar itens para restaurar sua saúde. Pague ${VALOR_DEBITO_SHOPPING}.",
                new EfeitoDebitoFixo(VALOR_DEBITO_SHOPPING)
            ),
            CartasCofre.CartaWhacka => new CartaCofre(
                $"Você bateu no whacka e recebeu um bump valioso para vendas e o vendeu na Toad's Town Recebe ${VALOR_CREDITO_WHACKA}.",
                new EfeitoCreditoFixo(VALOR_CREDITO_WHACKA)
            ),
            CartasCofre.CartaYoshiKids => new CartaCofre(
                $"Você conseguiu encontrar todos os yoshis kids perdidos na floresta. Parabéns.! Recebe ${VALOR_CREDITO_YOSHIKIDS}.",
                new EfeitoCreditoFixo(VALOR_CREDITO_YOSHIKIDS)
            ),

            _ => throw new ArgumentException($"ID de carta desconhecido: {cartaId}", nameof(cartaId))
        };

        return carta;
    }

    public List<CartaCofre> CriaTodasAsCartas()
    {
        return [
            CriaCarta(CartasCofre.CartaAntiGuys),
            CriaCarta(CartasCofre.CartaBandits),
            CriaCarta(CartasCofre.CartaChetRippo),
            CriaCarta(CartasCofre.CartaChuckQuizmo),
            CriaCarta(CartasCofre.CartaKetchnkoopa),
            CriaCarta(CartasCofre.CartaKoopaKoot),
            CriaCarta(CartasCofre.CartaMerlee),
            CriaCarta(CartasCofre.CartaMerluvelee),
            CriaCarta(CartasCofre.CartaMistake),
            CriaCarta(CartasCofre.CartaMistar),
            CriaCarta(CartasCofre.CartaPlayground),
            CriaCarta(CartasCofre.CartaRalph),
            CriaCarta(CartasCofre.CartaRipCheato),
            CriaCarta(CartasCofre.CartaShopping),
            CriaCarta(CartasCofre.CartaWhacka),
            CriaCarta(CartasCofre.CartaYoshiKids),
        ];
    }
}
