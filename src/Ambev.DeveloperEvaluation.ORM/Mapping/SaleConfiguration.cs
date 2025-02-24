using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.DateSale).IsRequired();
            builder.Property(s => s.IdCustomer).IsRequired();
            builder.Property(s => s.ValueTotal).IsRequired();
            builder.Property(s => s.Branch).IsRequired();

            builder.Property(u => u.Status)
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}
