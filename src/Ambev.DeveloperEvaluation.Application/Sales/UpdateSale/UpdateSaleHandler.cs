using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for UpdateSaleCommand</param>
    public UpdateSaleHandler(IMapper mapper,
                             ISaleRepository saleRepository)
    {
        _mapper = mapper ?? throw new ArgumentException(nameof(IMapper));
        _saleRepository = saleRepository ?? throw new ArgumentException(nameof(ISaleRepository));
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

        entidade.Status = SaleStatus.Active;
        entidade.DateUpdate = DateTime.UtcNow;

        var updateSale = await _saleRepository.UpdateAsync(entidade, cancellationToken);
        if (updateSale == null) throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

        var result = _mapper.Map<UpdateSaleResult>(updateSale);

        return result;
    }

    
}
