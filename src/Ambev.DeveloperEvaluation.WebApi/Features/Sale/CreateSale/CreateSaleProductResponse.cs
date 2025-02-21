namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleProductResponse
    {
        public Guid Id { get; set; }
        public Guid IdSale { get; set; }
        public Guid IdProduct { get; set; }
        public int Quantity { get; set; }
        public double PricesUnit { get; set; }
        public double PricesTotal { get; set; }
        public string Discounts { get; set; } = string.Empty;
    }
}
