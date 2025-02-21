using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public DateTime DateSale { get; set; }
    public Guid IdCustomer { get; set; }
    public double ValueTotal { get; set; }
    public Guid Branch { get; set; }
    public bool Cancelled { get; set; }
    public List<SaleProduct> Product { get; set; } = new();    
}
