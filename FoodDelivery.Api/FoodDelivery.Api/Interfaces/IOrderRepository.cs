using FoodDelivery.Api.Models;

namespace FoodDelivery.Api.Interfaces;

/// <summary>
/// Defines the contract for order persistence and retrieval operations.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Retrieves all orders asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation and returns a collection of orders.</returns>
    Task<IEnumerable<Order>> GetAllOrdersAsync();

    /// <summary>
    /// Retrieves an order by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <returns>A task that represents the asynchronous operation and returns the matching order, if found.</returns>
    Task<Order?> GetOrderByIdAsync(int id);

    /// <summary>
    /// Searches for orders by customer name and/or status asynchronously.
    /// </summary>
    /// <param name="customerName">The customer name to filter by. Optional.</param>
    /// <param name="status">The order status to filter by. Optional.</param>
    /// <returns>A task that represents the asynchronous operation and returns the matching orders.</returns>
    Task<IEnumerable<Order>> SearchOrdersAsync(string? customerName, OrderStatus? status);

    /// <summary>
    /// Adds a new order asynchronously.
    /// </summary>
    /// <param name="order">The order to add.</param>
    /// <returns>A task that represents the asynchronous operation and returns the created order.</returns>
    Task<Order> AddOrderAsync(Order order);

    /// <summary>
    /// Updates an existing order asynchronously.
    /// </summary>
    /// <param name="order">The order to update.</param>
    /// <returns>A task that represents the asynchronous operation and returns a value indicating whether the update succeeded.</returns>
    Task<bool> UpdateOrderAsync(Order order);

    /// <summary>
    /// Deletes an order by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the order to delete.</param>
    /// <returns>A task that represents the asynchronous operation and returns a value indicating whether the deletion succeeded.</returns>
    Task<bool> DeleteOrderAsync(int id);

    /// <summary>
    /// Updates the status of an existing order asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <param name="status">The new order status.</param>
    /// <returns>A task that represents the asynchronous operation and returns a value indicating whether the update succeeded.</returns>
    Task<bool> UpdateOrderStatusAsync(int id, OrderStatus status);

    /// <summary>
    /// Retrieves dashboard summary information asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation and returns the dashboard summary data.</returns>
    Task<DashboardSummaryDto> GetDashboardSummaryAsync();
}
