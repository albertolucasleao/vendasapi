using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleResponse
    {
        public Guid Id { get; set; }
        public DateTime DateSale { get; set; }
        public Guid IdCustomer { get; set; }
        public double ValueTotal { get; set; }
        public Guid Branch { get; set; }
        public bool Cancelled { get; set; }
        public List<CreateSaleProductResponse> Product { get; set; } = new();
    }
}
