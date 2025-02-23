using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Bus;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_mapper, _saleRepository, _rabbitMqProducer);
    }

    /// <summary>
    /// Tests that a valid user creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    [Trait("Category", "Create Sale Handler Test")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        
        var valorTotal = new double();
        command.Products.ForEach(x => { valorTotal += x.Quantity * x.PricesUnit; });

        var products = command.Products.Select(p => new SaleProduct {
            IdProduct = p.IdProduct,
            Quantity = p.Quantity,
            PricesUnit = p.PricesUnit
        }).ToList();

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            DateSale = DateTime.UtcNow,
            DateUpdate = null,
            IdCustomer = command.IdCustomer,
            ValueTotal = valorTotal,
            Branch = command.Branch,
            Status = SaleStatus.Active,
            Products = products
        };

        var result = new CreateSaleResult
        {
            Id = sale.Id,
            DateSale = sale.DateSale,
            IdCustomer = sale.IdCustomer,
            ValueTotal = sale.ValueTotal,
            Branch = sale.Branch,
            Status = sale.Status,
            Products = _mapper.Map<List<CreateSaleProductResult>>(sale.Products)
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    [Trait("Category", "Create Sale Handler Test")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    [Trait("Category", "Create Sale Handler Test")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();

        var valorTotal = new double();
        command.Products.ForEach(x => { valorTotal += x.Quantity * x.PricesUnit; });

        var products = command.Products.Select(p => new SaleProduct
        {
            IdProduct = p.IdProduct,
            Quantity = p.Quantity,
            PricesUnit = p.PricesUnit
        }).ToList();

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            DateSale = DateTime.UtcNow,
            DateUpdate = null,
            IdCustomer = command.IdCustomer,
            ValueTotal = valorTotal,
            Branch = command.Branch,
            Status = SaleStatus.Active,
            Products = products
        };

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<Sale>(Arg.Is<CreateSaleCommand>(c =>
            c.IdCustomer == command.IdCustomer &&
            c.Branch == command.Branch &&
            c.Products == command.Products));
    }
}
