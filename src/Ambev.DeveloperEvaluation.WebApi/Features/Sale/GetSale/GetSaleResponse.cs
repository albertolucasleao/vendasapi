using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public DateTime DateSale { get; set; }
        public Guid IdCustomer { get; set; }
        public double ValueTotal { get; set; }
        public Guid Branch { get; set; }
        public bool Cancelled { get; set; }
        public List<GetSaleProductResponse> Product { get; set; } = new();
    }
}
