using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handler for processing DeleteSaleCommand requests
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IRabbitMqProducer _producer;

    /// <summary>
    /// Initializes a new instance of DeleteSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for DeleteSaleCommand</param>
    public DeleteSaleHandler(ISaleRepository saleRepository,
                             IRabbitMqProducer producer)
    {
        _saleRepository = saleRepository ?? throw new ArgumentException(nameof(ISaleRepository));
        _producer = producer ?? throw new ArgumentException(nameof(IRabbitMqProducer));
    }

    /// <summary>
    /// Handles the DeleteSaleCommand request
    /// </summary>
    /// <param name="command">The DeleteSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The delete sale details</returns>
    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var entidade = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entidade == null) throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        var success = await _saleRepository.DeleteAsync(entidade, cancellationToken);
        if (!success) throw new KeyNotFoundException($"Sale with ID {request.Id} not deleted");
        
        _producer.Publish("delete_sale", entidade);

        return new DeleteSaleResult { Success = true };
    }
}
