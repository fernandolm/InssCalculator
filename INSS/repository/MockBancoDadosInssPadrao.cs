using INSS.repository.interfaces;
using INSS.domain;
using System;

namespace INSS.repository
{
    internal class MockBancoDadosInssPadrao : IMockBancoDadosInss
    {
        public ContextoInss CriarMock()
        {
            Console.WriteLine("-> Criando mock Padrão");
            return CarregarMockPadrao();
        }

        private ContextoInss CarregarMockPadrao()
        {
            var contextoInss = BancoDadosInss.CriarBancoDadosEmMemoria();
            Console.WriteLine("-> Banco de dados Padrão criado com sucesso.");

            //DadosInss
            contextoInss.DadosInss.Add(new DadosInss()
            {
                AnoContribuicao = 2011,
                ValorTetoDesconto = (decimal)405.86
            });

            contextoInss.DadosInss.Add(new DadosInss()
            {
                AnoContribuicao = 2012,
                ValorTetoDesconto = (decimal)500
            });

            Console.WriteLine("-> Dados Inss inseridos com sucesso.");

            //FaixasInss 2011
            contextoInss.FaixasInss.Add(new FaixasInss()
            {
                AnoContribuicao = 2011,
                ValorContribuicaoInicial = 0,
                ValorContribuicaoFinal = (decimal)1106.90,
                Aliquota = 8
            });

            contextoInss.FaixasInss.Add(new FaixasInss()
            {
                AnoContribuicao = 2011,
                ValorContribuicaoInicial = (decimal)1106.91,
                ValorContribuicaoFinal = (decimal)1844.83,
                Aliquota = 9
            });

            contextoInss.FaixasInss.Add(new FaixasInss()
            {
                AnoContribuicao = 2011,
                ValorContribuicaoInicial = (decimal)1844.84,
                ValorContribuicaoFinal = (decimal)3689.66,
                Aliquota = 11
            });

            //FaixasInss 2012
            contextoInss.FaixasInss.Add(new FaixasInss()
            {
                AnoContribuicao = 2012,
                ValorContribuicaoInicial = 0,
                ValorContribuicaoFinal = (decimal)1000,
                Aliquota = 7
            });

            contextoInss.FaixasInss.Add(new FaixasInss()
            {
                AnoContribuicao = 2012,
                ValorContribuicaoInicial = (decimal)1000.01,
                ValorContribuicaoFinal = (decimal)1500,
                Aliquota = 8
            });

            contextoInss.FaixasInss.Add(new FaixasInss()
            {
                AnoContribuicao = 2012,
                ValorContribuicaoInicial = (decimal)1500.01,
                ValorContribuicaoFinal = (decimal)3000,
                Aliquota = 9
            });

            contextoInss.FaixasInss.Add(new FaixasInss()
            {
                AnoContribuicao = 2012,
                ValorContribuicaoInicial = (decimal)3000.01,
                ValorContribuicaoFinal = (decimal)4000,
                Aliquota = 11
            });

            Console.WriteLine("-> Faixas Inss inseridos com sucesso.");

            contextoInss.SaveChanges();
            Console.WriteLine("-> Inserções salvas com sucesso.");

            return contextoInss;
        }
    }
}
