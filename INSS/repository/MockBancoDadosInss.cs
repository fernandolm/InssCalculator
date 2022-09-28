using INSS.repository.interfaces;

namespace INSS.repository
{
    internal static class MockBancoDadosInss
    {
        public static ContextoInss CarregarDados()
        {
            IMockBancoDadosInss mockBancoDadosInss = MockBancoDadosInssFactory.CriarMockBancoDadosInss();
            return mockBancoDadosInss.CriarMock();
        }
    }
}
