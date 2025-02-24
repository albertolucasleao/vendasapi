using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Bus;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.SaleTestsData.Create;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.SaleTestsData.Entities;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTests.GetSale;

// <summary>
/// Contains unit tests for the <see cref="GetSaleHandler"/> class.
/// </summary>
public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly GetSaleHandler _handler;
    private readonly IRabbitMqProducer _rabbitMqProducer;
    private readonly IMapper _mapper;


    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _rabbitMqProducer = Substitute.For<IRabbitMqProducer>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleHandler(_mapper, _saleRepository, _rabbitMqProducer);
    }

    /// <summary>
    /// Tests that a valid sale get request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When get sale Then returns success response")]
    [Trait("Category", "Sale Test")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new GetSaleCommand(Guid.NewGuid());
        var sale = SaleTestData.GenerateSaleValid();

        var saleProduct = sale.Products.FirstOrDefault();

        var resultProduct = new List<GetSaleProductResult>
        {
            new GetSaleProductResult
            {
                Id = saleProduct.Id,
                IdSale = saleProduct.IdSale,
                IdProduct = saleProduct.IdProduct,
                Quantity = saleProduct.Quantity,
                PricesUnit = saleProduct.PricesUnit,
                PricesTotal = saleProduct.PricesTotal,
                Discount = saleProduct.Discount,
                TotalPaid = saleProduct.TotalPaid
            }
        };

        var result = new GetSaleResult
        {
            Id = sale.Id,
            DateSale = sale.DateSale,
            IdCustomer = sale.IdCustomer,
            ValueTotal = sale.ValueTotal,
            Branch = sale.Branch,
            Status = sale.Status,
            Products = resultProduct
        };

        _mapper.Map<GetSaleResult>(sale).Returns(result);

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        await _saleRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale get request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When get sale Then throws validation exception")]
    [Trait("Category", "Sale Test")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetSaleCommand(Arg.Any<Guid>()); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

}
