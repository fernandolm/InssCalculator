using INSS.repository.interfaces;
using System.IO;
using System.Configuration;

namespace INSS.repository
{
    internal static class MockBancoDadosInssFactory
    {
        private static readonly string AppSettingsMockDiretorio = "MockDiretorio";
        private static readonly string AppSettingsMockArquivoDadosInss = "MockArquivoDadosInss";
        private static readonly string AppSettingsMockArquivoFaixasInss = "MockArquivoFaixasInss";

        public static IMockBancoDadosInss CriarMockBancoDadosInss() {
            if (ObterTipoMockJson())
            {
                return new MockBancoDadosInssJson();
            }
            else
            {
                return new MockBancoDadosInssPadrao();
            }
        }

        private static bool ObterTipoMockJson()
        {
            var caminhoConfiguracaoJson = Directory.GetCurrentDirectory() + "\\" + ConfigurationManager.AppSettings[AppSettingsMockDiretorio] + "\\";

            bool mockJson = File.Exists(caminhoConfiguracaoJson + ConfigurationManager.AppSettings[AppSettingsMockArquivoDadosInss]) &&
                File.Exists(caminhoConfiguracaoJson + ConfigurationManager.AppSettings[AppSettingsMockArquivoFaixasInss]);

            return mockJson;
        }
    }
}
