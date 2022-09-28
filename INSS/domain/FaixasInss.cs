using System.ComponentModel.DataAnnotations;

namespace INSS.domain
{
    internal class FaixasInss
    {
        [Key]
        public int Id { get; set; }
        public int AnoContribuicao { get; set; }
        public decimal ValorContribuicaoInicial { get; set; }
        public decimal ValorContribuicaoFinal { get; set; }
        public decimal Aliquota { get; set; }
    }
}
