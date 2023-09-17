public class Boleto {
    public string Numero { get; set; }
    public double Valor { get; set; }
    public DateTime DataPagamento { get; set; }
    public bool Pago { get; set; }
    public void Pagar(ContaCorrente contaCorrente) {
        contaCorrente.Sacar(this.Valor);
        this.Pago = true;
        this.DataPagamento = DateTime.Now;
    }

    public static Boleto Criar(string numero, double valor) {
        return new Boleto() {
            Numero = numero,
            Valor = valor,
            Pago = false
        };
    }
}