using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleRequest
    {
        public Guid IdCustomer { get; set; }
        public Guid Branch { get; set; }
        public List<CreateSaleProductRequest> Product { get; set; } = new();
    }
}
