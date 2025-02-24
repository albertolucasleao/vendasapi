using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Bus;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.SaleTestsData.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTests.DeleteSale;

// <summary>
/// Contains unit tests for the <see cref="DeleteSaleHandler"/> class.
/// </summary>
public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteSaleHandler _handler;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _rabbitMqProducer = Substitute.For<IRabbitMqProducer>();
        _handler = new DeleteSaleHandler(_saleRepository, _rabbitMqProducer);
    }

    /// <summary>
    /// Tests that a valid sale delete request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When delete sale Then returns success response")]
    [Trait("Category", "Sale Test")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new DeleteSaleCommand { Id = Guid.NewGuid() };
        var sale = SaleTestData.GenerateSaleValid();

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.DeleteAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Success.Equals(true);
        await _saleRepository.Received(1).DeleteAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale delete request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    [Trait("Category", "Sale Test")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new DeleteSaleCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
}
