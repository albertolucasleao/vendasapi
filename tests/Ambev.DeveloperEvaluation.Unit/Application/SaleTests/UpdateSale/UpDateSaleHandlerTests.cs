using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Bus;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.SaleTestsData.Entities;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTests.UpdateSale;

// <summary>
/// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
/// </summary>
public class UpDateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly UpdateSaleHandler _handler;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public UpDateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _rabbitMqProducer = Substitute.For<IRabbitMqProducer>();
        _handler = new UpdateSaleHandler(_mapper, _saleRepository, _rabbitMqProducer);
    }

    /// <summary>
    /// Tests that a valid sale creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    [Trait("Category", "Sale Test")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new UpdateSaleCommand { Id = Guid.NewGuid() };
        var sale = SaleTestData.GenerateSaleValid();

        var saleProduct = sale.Products.FirstOrDefault();

        var resultProduct = new List<UpdateSaleProductResult>
        {
            new UpdateSaleProductResult
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

        var result = new UpdateSaleResult
        {
            Id = sale.Id,
            DateSale = sale.DateSale,
            IdCustomer = sale.IdCustomer,
            ValueTotal = sale.ValueTotal,
            Branch = sale.Branch,
            Status = sale.Status,
            Products = resultProduct
        };

        _mapper.Map<UpdateSaleResult>(sale).Returns(result);

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale update request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When update sale Then throws validation exception")]
    [Trait("Category", "Sale Test")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}
