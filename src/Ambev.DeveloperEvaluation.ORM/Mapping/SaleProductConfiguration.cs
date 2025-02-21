using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleProductConfiguration : IEntityTypeConfiguration<SaleProduct>
{
    public void Configure(EntityTypeBuilder<SaleProduct> builder)
    {
        builder.ToTable("SaleProducts");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.IdProduct).IsRequired();
        builder.Property(s => s.Quantity).IsRequired();
        builder.Property(s => s.PricesUnit).IsRequired();
        builder.Property(s => s.PricesTotal).IsRequired();
        builder.Property(s => s.TotalPaid);
        builder.Property(s => s.Discount);

        builder.HasOne(s => s.Sale).WithMany(s => s.Product)
            .HasForeignKey(s => s.IdSale)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
