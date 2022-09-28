using INSS.repository;
using INSS.domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INSS.service
{
    public class CalculadorInss : ICalculadorInss
    {
        private readonly string ErroAnoContribuicaoInexistente = "O ano de contribuição não está cadastrado. Ano: {0}";

        decimal ICalculadorInss.CalcularDesconto(DateTime data, decimal salario)
        {
            const int TotalLinha = 50;
            decimal desconto = 0;
            var dadosInss = new List<DadosInss>();
            var faixasInss = new List<FaixasInss>();
            var anoContribuicao = data.Year;

            Console.WriteLine(new String('-', TotalLinha));
            Console.WriteLine("Log:");

            CriarMockBancoDadosInss(ref dadosInss, ref faixasInss);
            Console.WriteLine(String.Format("-> Total de Dados Inss = {0}", dadosInss.Count));
            Console.WriteLine(String.Format("-> Total de Faixas Inss = {0}", faixasInss.Count));

            var dadoInss = dadosInss.Find(d => d.AnoContribuicao == anoContribuicao);

            if (dadoInss == null) {
                throw new Exception(string.Format(ErroAnoContribuicaoInexistente, anoContribuicao));
            }

            var faixaInss = faixasInss.Find(f => f.AnoContribuicao == anoContribuicao && 
                (salario >= f.ValorContribuicaoInicial && salario <= f.ValorContribuicaoFinal));

            if (faixaInss != null)
            {
                Console.WriteLine(String.Format("-> Realizando cálculo do desconto pela alíquota {0}%.", faixaInss.Aliquota));
                desconto = salario * (faixaInss.Aliquota / 100);
            }
            else {
                Console.WriteLine(String.Format("-> Desconto é igual ao teto R${0}.", dadoInss.ValorTetoDesconto));
                desconto = dadoInss.ValorTetoDesconto;
            }

            Console.WriteLine(new String('-', TotalLinha));

            return desconto;
        }

        private void CriarMockBancoDadosInss(ref List<DadosInss> dadosInss, ref List<FaixasInss> faixasInss) {
            var contextoInss = MockBancoDadosInss.CarregarDados();

            dadosInss = contextoInss.DadosInss.ToList<DadosInss>();
            faixasInss = contextoInss.FaixasInss.ToList<FaixasInss>();
        }
    }
}
