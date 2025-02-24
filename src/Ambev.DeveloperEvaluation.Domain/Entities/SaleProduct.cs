using Ambev.DeveloperEvaluation.Domain.Common;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleProduct : BaseEntity
{
    public Guid IdProduct { get; set; }
    public int Quantity { get; set; }
    public double PricesUnit { get; set; }
    public double Discount { get; set; }
    public double TotalPaid { get; set; }
    
    public double PricesTotal { get; set; }

    public Guid IdSale { get; set; }
    [JsonIgnore]
    public Sale Sale { get; set; }

    public void ApplyDiscount()
    {
        if (Quantity >= 4 && Quantity <= 9)
        {
            Discount = 0.10; // 10%
            TotalPaid = Quantity * PricesUnit * (1 - Discount);
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = 0.20; // 20%
            TotalPaid = Quantity * PricesUnit * (1 - Discount);
        }
        else 
        {
            Discount = 0;
            TotalPaid = Quantity * PricesUnit;
        }            
    }

    public void CalculateTotal() 
    {
        PricesTotal = Quantity * PricesUnit;
    }
}
