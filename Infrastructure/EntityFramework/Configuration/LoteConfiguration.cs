using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configuration
{
    public class LoteConfiguration : IEntityTypeConfiguration<Lote>
    {
        public void Configure(EntityTypeBuilder<Lote> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(l => l.Codigo).IsRequired();
            builder.Property(l => l.Quantidade).IsRequired();
            builder.Property(l => l.Fabricacao);
            builder.Property(l => l.Validade).IsRequired();
            builder.HasOne(l => l.Produto).WithMany(p => p.Lotes).HasForeignKey(l => l.ProdutoId);
        }
    }
}