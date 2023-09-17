using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;
using System.ComponentModel;
using System;
using System.Globalization;
using System.Collections.Generic;

public class BankSkills {

    public BankSkills(ContaCorrente contaCorrentePrincipal, Boleto[] boletosPendentes, Dictionary<string, ContaCorrente> contasRelacionadas) {
        this.contaCorrentePrincipal = contaCorrentePrincipal;
        this.boletosPendentes = boletosPendentes; 
        this.contasRelacionadas = contasRelacionadas;
    }

    Dictionary<string, ContaCorrente> contasRelacionadas {get; set;}
    ContaCorrente contaCorrentePrincipal {get; set;}
    Boleto[] boletosPendentes {get; set;}
  
    [SKFunction, Description("Obtem o Saldo da Conta corrente do usuario atual")]
    public string Saldo()
    {
        System.Console.WriteLine("Saldo: " + contaCorrentePrincipal.Saldo);
      return (contaCorrentePrincipal.Saldo).ToString(CultureInfo.InvariantCulture);
    }


    [SKFunction, Description("Obtem os boletos pendentes do usuario atual")]
    public string BoletosPendentes()
    {
        string retVal = "Boletos Pendentes: \n";
        foreach(var boleto in this.boletosPendentes)
        {
            if (!boleto.Pago) {
                retVal += "Numero:" + boleto.Numero + " - Valor:" + boleto.Valor + "\n";
            }
            
        }
        System.Console.WriteLine(retVal);
        return retVal;
    }

    [SKFunction, Description("Obtem os boletos pendentes do usuario atual")]
    [SKParameter("input", "Numero do Boleto")]
    public string PagarBoleto(string numeroBoleto)
    {
        System.Console.WriteLine("Pagar Boleto " + numeroBoleto + " chamado");
        foreach(var boleto in this.boletosPendentes)
        {
            if (boleto.Numero == numeroBoleto) {
                if (boleto.Pago) {
                    return "Boleto " + numeroBoleto + " ja foi pago!";
                }
                boleto.Pagar(contaCorrentePrincipal);
                return "Boleto " + numeroBoleto + " pago com sucesso!";
            }
        }
        return "Boleto " + numeroBoleto + " nao encontrado!";
    }

    [SKFunction, Description("Obtem os numeros de contas relacionadas ao usuario atual pelo nome ou apelido")]
    [SKParameter("input", "Nome da Conta")]
    public string ObterContaRelacionada(string nomeConta)
    {
        System.Console.WriteLine("ObterContaRelacionada " + nomeConta + " chamado");
        if (contasRelacionadas.ContainsKey(nomeConta)) {
            return "Conta " + nomeConta + " encontrada! Número da Conta: " + contasRelacionadas[nomeConta].Numero + " - E-mail para comprovantes:" + contasRelacionadas[nomeConta].EmailComprovante;
        } 
        return "Conta " + nomeConta + " não encontrada!";
    }

    [SKFunction, Description("Transferir um valor para uma conta relacionada")]
    [SKParameter("input", "Numero da Conta relacionada")]
    [SKParameter("valor", "Valor a ser transferido")]
    public string Transferir(SKContext context)
    {
        string numeroConta = context.Variables["input"];
        string valor = context.Variables["valor"];
        System.Console.WriteLine("Transferir " + valor + " para " + numeroConta + " chamado");
        foreach(var conta in contasRelacionadas.Values)
        {
            if (conta.Numero == numeroConta) {
                contaCorrentePrincipal.Transferir(Convert.ToDouble(valor, CultureInfo.InvariantCulture), conta);
                return "Transferencia de " + valor + " para " + numeroConta + " realizada com sucesso!";
            }
        }
        return "Conta " + numeroConta + " não encontrada!";
    }

    

}