namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleProductRequest
    {
        public Guid IdProduct { get; set; }
        public int Quantity { get; set; }
        public double PricesUnit { get; set; }
    }
}
