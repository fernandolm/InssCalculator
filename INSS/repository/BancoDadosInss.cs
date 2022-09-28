using Microsoft.EntityFrameworkCore;

namespace INSS.repository
{
    internal static class BancoDadosInss
    {
        public static ContextoInss CriarBancoDadosEmMemoria() {
            var opcoes = new DbContextOptionsBuilder<ContextoInss>().UseInMemoryDatabase("Inss").Options;
            var contextoInss = new ContextoInss(opcoes);

            if (contextoInss != null)
            {
                contextoInss.Database.EnsureDeleted();
                contextoInss.Database.EnsureCreated();
            }

            return contextoInss;
        }
    }
}
