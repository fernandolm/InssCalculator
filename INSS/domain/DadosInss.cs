using System.ComponentModel.DataAnnotations;

namespace INSS.domain
{
    internal class DadosInss
    {
        [Key]
        public int AnoContribuicao { get; set; }
        public decimal ValorTetoDesconto { get; set; }
    }
}
