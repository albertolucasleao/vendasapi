using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Bus;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly IRabbitMqProducer _producer;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateSaleCommand</param>
    public CreateSaleHandler(IMapper mapper, 
                             ISaleRepository saleRepository,
                             IRabbitMqProducer producer)
    {
        _mapper = mapper ?? throw new ArgumentException(nameof(IMapper));
        _saleRepository = saleRepository ?? throw new ArgumentException(nameof(ISaleRepository));
        _producer = producer ?? throw new ArgumentException(nameof(IRabbitMqProducer));
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validatorSale = new CreateSaleValidator();
        var validationSaleResult = await validatorSale.ValidateAsync(command, cancellationToken);

        if (!validationSaleResult.IsValid)
            throw new ValidationException(validationSaleResult.Errors);

        var sale = _mapper.Map<Sale>(command);
        
        sale.DateSale = DateTime.UtcNow;

        sale.Products.ForEach(x => { x.ApplyDiscount(); });
        sale.Products.ForEach(x => { x.CalculateTotal(); });

        var valorTotal = new double();
        sale.Products.ForEach(x => { valorTotal += x.TotalPaid; });
        sale.ValueTotal = valorTotal;

        sale.Status = SaleStatus.Active;

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        _producer.Publish("create_sale", createdSale);

        var result = _mapper.Map<CreateSaleResult>(createdSale);

        return result;
    }
}
