using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;
using System.ComponentModel;
using System;
using System.Globalization;
using System.Collections.Generic;

public class EmailSkills {

    public EmailSkills() {
    }



    [SKFunction, Description("Enviar um e-mail")]
    [SKParameter("input", "E-mail do destinatario")]
    [SKParameter("texto", "Texto do e-mail")]
    public string EnviarEmail(SKContext context)
    {
        string email = context.Variables["input"];
        string texto = context.Variables["texto"];
        System.Console.WriteLine("Enviar e-mail para: " + email + "\nTexto: " + texto);
        return "E-mail enviado com Sucesso";
    }

    

}