using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Date of sale.
    /// Must not be null or empty.
    /// </summary>
    public DateTime DateSale { get; set; }

    /// <summary>
    /// Date of update.
    /// Must not be null or empty.
    /// </summary>
    public DateTime? DateUpdate { get; set; }

    /// <summary>
    /// Customer identification.
    /// Must not be null or empty.
    /// </summary>
    public Guid IdCustomer { get; set; }

    /// <summary>
    /// Total purchase price, including discounts.
    /// Must not be null or empty.
    /// </summary>
    public double ValueTotal { get; set; }

    /// <summary>
    /// Branch Identification.
    /// Must not be null or empty.
    /// </summary>
    public Guid Branch { get; set; }

    /// <summary>
    /// The sale is canceled.
    /// Must not be null or empty.
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// Sale products.
    /// Must not be null or empty.
    /// </summary>
    public List<SaleProduct> Products { get; set; } = new();

    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}
