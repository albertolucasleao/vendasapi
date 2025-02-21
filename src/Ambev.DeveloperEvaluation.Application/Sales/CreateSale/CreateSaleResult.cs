namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleResult
{
    public Guid Id { get; set; }
    public DateTime DateSale { get; set; }
    public Guid IdCustomer { get; set; }
    public double ValueTotal { get; set; }
    public Guid Branch { get; set; }
    public bool Cancelled { get; set; }
    public List<CreateSaleProductResult> Product { get; set; } = new();
}
