namespace FoodDelivery.Api.Models;

/// <summary>
/// Represents the available lifecycle statuses for an order.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Order has been placed and is awaiting processing.
    /// </summary>
    Placed = 1,

    /// <summary>
    /// Order is being prepared by the restaurant.
    /// </summary>
    Preparing = 2,

    /// <summary>
    /// Order is out for delivery.
    /// </summary>
    OutForDelivery = 3,

    /// <summary>
    /// Order has been delivered successfully.
    /// </summary>
    Delivered = 4,

    /// <summary>
    /// Order was cancelled before completion.
    /// </summary>
    Cancelled = 5
}
