public class ContaCorrente {

    public ContaCorrente(string numero, string email)  {
        this.Saldo = 0;
        this.Numero = numero;
        this.EmailComprovante = email;
    }

    public string Numero { get; set; }


    public double Saldo { get; set; }

    public string EmailComprovante { get; set; }

    

    public void Depositar(double valor) {
        this.Saldo += valor;
    }

    public void Sacar(double valor) {
        this.Saldo -= valor;
    }

    public void Transferir(double valor, ContaCorrente contaDestino) {
        this.Sacar(valor);
        contaDestino.Depositar(valor);
    }
}