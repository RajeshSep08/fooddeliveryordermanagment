using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Api.Models;

/// <summary>
/// Represents a food delivery order managed by the system.
/// </summary>
public class Order
{
    /// <summary>
    /// Gets or sets the unique identifier for the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the customer name for the order.
    /// </summary>
    [Required(ErrorMessage = "Customer name is required.")]
    [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer phone number for the order.
    /// </summary>
    [Required(ErrorMessage = "Customer phone is required.")]
    [StringLength(20, ErrorMessage = "Customer phone cannot exceed 20 characters.")]
    public string CustomerPhone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the food item requested in the order.
    /// </summary>
    [Required(ErrorMessage = "Food item is required.")]
    [StringLength(100, ErrorMessage = "Food item cannot exceed 100 characters.")]
    public string FoodItem { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the food item requested.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the price of the order.
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the delivery address for the order.
    /// </summary>
    [Required(ErrorMessage = "Delivery address is required.")]
    [StringLength(250, ErrorMessage = "Delivery address cannot exceed 250 characters.")]
    public string DeliveryAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current status of the order.
    /// </summary>
    [Required]
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was placed.
    /// </summary>
    [Required]
    public DateTime OrderDate { get; set; } =  DateTime.UtcNow;
}
