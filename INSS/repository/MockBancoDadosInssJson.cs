using INSS.repository.interfaces;
using INSS.domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using System.Configuration;
using Newtonsoft.Json.Serialization;
using INSS.converter;

namespace INSS.repository
{
    internal class MockBancoDadosInssJson : IMockBancoDadosInss
    {
        private readonly string AppSettingsMockDiretorio = "MockDiretorio";
        private readonly string AppSettingsMockArquivoDadosInss = "MockArquivoDadosInss";
        private readonly string AppSettingsMockArquivoFaixasInss = "MockArquivoFaixasInss";

        public ContextoInss CriarMock()
        {
            Console.WriteLine("-> Criando mock Json");
            return CarregarMockJson();
        }

        private ContextoInss CarregarMockJson() {
            var dadosInss = ObterMockDadosInss();
            var faixasInss = ObterMockFaixasInss();

            var contextoInss = BancoDadosInss.CriarBancoDadosEmMemoria();
            Console.WriteLine("-> Banco de dados Json criado com sucesso.");

            InserirDadosInss(dadosInss, contextoInss);
            Console.WriteLine("-> Dados Inss inseridos com sucesso.");

            InserirFaixasInss(faixasInss, contextoInss);
            Console.WriteLine("-> Faixas Inss inseridos com sucesso.");

            contextoInss.SaveChanges();

            return contextoInss;
        }

        private void InserirDadosInss(List<DadosInss> dadosInss, ContextoInss contextoInss) {
            foreach(DadosInss dadoInss in dadosInss) {
                contextoInss.DadosInss.Add(new DadosInss()
                {
                    AnoContribuicao = dadoInss.AnoContribuicao,
                    ValorTetoDesconto = dadoInss.ValorTetoDesconto
                });
            }
        }

        private void InserirFaixasInss(List<FaixasInss> faixasInss, ContextoInss contextoInss)
        {
            foreach (FaixasInss faixaInss in faixasInss)
            {
                contextoInss.FaixasInss.Add(new FaixasInss()
                {
                    AnoContribuicao = faixaInss.AnoContribuicao,
                    ValorContribuicaoInicial = faixaInss.ValorContribuicaoInicial,
                    ValorContribuicaoFinal = faixaInss.ValorContribuicaoFinal,
                    Aliquota = faixaInss.Aliquota
                });
            }
        }

        private List<DadosInss> ObterMockDadosInss()
        {
            var caminhoConfiguracaoJson = Directory.GetCurrentDirectory() + "\\" + ConfigurationManager.AppSettings[AppSettingsMockDiretorio] + "\\";
            var caminhoMockDadosInss = caminhoConfiguracaoJson + ConfigurationManager.AppSettings[AppSettingsMockArquivoDadosInss];

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            var dadosInssJson = File.ReadAllText($@"{caminhoMockDadosInss}");
            List<DadosInss> dadosInss = JsonConvert.DeserializeObject<List<DadosInss>>(dadosInssJson, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new DecimalConverter() }
            });

            return dadosInss;
        }

        private List<FaixasInss> ObterMockFaixasInss()
        {
            var caminhoConfiguracaoJson = Directory.GetCurrentDirectory() + "\\" + ConfigurationManager.AppSettings[AppSettingsMockDiretorio] + "\\";
            var caminhoMockFaixasInss = caminhoConfiguracaoJson + ConfigurationManager.AppSettings[AppSettingsMockArquivoFaixasInss];

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            var faixasInssJson = File.ReadAllText($@"{caminhoMockFaixasInss}");
            List<FaixasInss> faixasInss = JsonConvert.DeserializeObject<List<FaixasInss>>(faixasInssJson, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new DecimalConverter() }
            });

            return faixasInss;
        }
    }
}
