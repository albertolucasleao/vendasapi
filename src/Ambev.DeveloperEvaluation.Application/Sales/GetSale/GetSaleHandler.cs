using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public GetSaleHandler(IMapper mapper,
                              ISaleRepository saleRepository)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(IMapper));
            _saleRepository = saleRepository ?? throw new ArgumentException(nameof(ISaleRepository));
        }

        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");



            return _mapper.Map<GetSaleResult>(sale);
        }
    }
}
