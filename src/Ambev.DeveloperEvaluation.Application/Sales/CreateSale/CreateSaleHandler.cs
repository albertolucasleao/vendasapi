using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;

    public CreateSaleHandler(IMapper mapper, 
                             ISaleRepository saleRepository)
    {
        _mapper = mapper ?? throw new ArgumentException(nameof(IMapper));
        _saleRepository = saleRepository ?? throw new ArgumentException(nameof(ISaleRepository));
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = _mapper.Map<Sale>(command);
        
        sale.DateSale = DateTime.UtcNow;

        sale.Product.ForEach(x => { x.ApplyDiscount(); });
        sale.Product.ForEach(x => { x.CalculateTotal(); });

        var valorTotal = new double();
        sale.Product.ForEach(x => { valorTotal =+ x.TotalPaid; });
        sale.ValueTotal = valorTotal;

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);

        return result;
    }
}
