using System.ComponentModel;

namespace eAgenda.Dominio.ModuloDespesa
{
    public enum FormaPgtoDespesaEnum
    {
        [Description("PIX")]
        PIX,

        [Description("Dinheiro")]
        Dinheiro,

        [Description("Cartão de Crédito")]
        CartaoCredito
    }

}
