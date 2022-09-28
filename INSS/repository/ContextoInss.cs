using INSS.domain;
using Microsoft.EntityFrameworkCore;

namespace INSS.repository
{
    internal class ContextoInss : DbContext
    {
        public ContextoInss(DbContextOptions<ContextoInss> options)
            : base(options) { }

        public virtual DbSet<DadosInss> DadosInss { get; set; }
        public virtual DbSet<FaixasInss> FaixasInss { get; set; }
    }
}
