using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;
    private readonly IRabbitMqProducer _producer;


    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for UpdateSaleCommand</param>
    public UpdateSaleHandler(IMapper mapper,
                             ISaleRepository saleRepository,
                             IRabbitMqProducer producer)
    {
        _mapper = mapper ?? throw new ArgumentException(nameof(IMapper));
        _saleRepository = saleRepository ?? throw new ArgumentException(nameof(ISaleRepository));
        _producer = producer ?? throw new ArgumentException(nameof(IRabbitMqProducer));
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="request">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details if found</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var entidade = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entidade == null) throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
        if (entidade.Status == SaleStatus.Canceled) { return _mapper.Map<UpdateSaleResult>(entidade); };

        entidade.Status = SaleStatus.Canceled;
        entidade.DateUpdate = DateTime.UtcNow;

        var updateSale = await _saleRepository.UpdateAsync(entidade, cancellationToken);
        if (updateSale == null) throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        var result = _mapper.Map<UpdateSaleResult>(updateSale);

        _producer.Publish("update_sale", entidade);

        return result;
    }

    
}
