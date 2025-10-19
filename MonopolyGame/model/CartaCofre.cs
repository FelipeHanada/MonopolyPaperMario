namespace MonopolyPaperMario.model
{
    // Certifique-se de que as propriedades são públicas para o JSON funcionar!
    public class CartaCofre : CartaInstantanea 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Texto { get; set; }
        // O JSON já tem o sinal correto (+ para receber, - para pagar)
        public int Valor { get; set; } 
        public string Tipo { get; set; }
    
        public override void QuandoPegada(Jogador jogador)
        {
            // Se Valor = 50, o jogador recebe 50.
            // Se Valor = -50, o jogador paga 50 (e a verificação de fundos é feita no método do jogador).
            
            try
            {
                jogador.mudarDinheiro(this.Valor);
            }
            catch (FundosInsuficientesException ex)
            {
                // Aqui você deve implementar a lógica de falência,
                // como forçar o jogador a vender propriedades ou hipotecar.
                Console.WriteLine($"O Jogador {jogador.getNome()} não tem fundos para pagar {Math.Abs(this.Valor)}. Iniciar processo de falência/venda.");
                // Se o jogo for mais simples, pode-se apenas forçar o dinheiro a zero:
                // jogador.setDinheiro(0);
                
                // Em um Monopoly completo, você lidaria com a exceção para vender ativos.
            }
        } 
    }
}