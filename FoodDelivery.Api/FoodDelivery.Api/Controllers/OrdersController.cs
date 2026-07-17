using System.ComponentModel.DataAnnotations;
using FoodDelivery.Api.Interfaces;
using FoodDelivery.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Provides API endpoints for managing food delivery orders.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersController"/> class.
    /// </summary>
    /// <param name="orderRepository">The repository used for order persistence.</param>
    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>A 200 OK response containing the list of orders.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Retrieves an order by its identifier.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <returns>A 200 OK response with the order, or 404 Not Found if missing.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    /// <summary>
    /// Searches orders by customer name and/or status.
    /// </summary>
    /// <param name="customerName">Optional customer name filter.</param>
    /// <param name="status">Optional order status filter.</param>
    /// <returns>A 200 OK response containing the matching orders.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> SearchOrders([FromQuery] string? customerName, [FromQuery] OrderStatus? status)
    {
        var orders = await _orderRepository.SearchOrdersAsync(customerName, status);
        return Ok(orders);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The order to create.</param>
    /// <returns>A 201 Created response with the created order.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdOrder = await _orderRepository.AddOrderAsync(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
    }

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <param name="order">The updated order data.</param>
    /// <returns>A 204 No Content response on success, or 400/404 for invalid requests.</returns>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != order.Id)
        {
            return BadRequest();
        }

        var updated = await _orderRepository.UpdateOrderAsync(order);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Updates the status of an existing order.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <param name="request">The status update request payload.</param>
    /// <returns>A 204 No Content response on success, or 400/404 for invalid requests.</returns>
    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusRequest request)
    {
        if (!ModelState.IsValid || request is null)
        {
            return BadRequest(ModelState);
        }

        var updated = await _orderRepository.UpdateOrderStatusAsync(id, request.Status);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Deletes an order by identifier.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <returns>A 204 No Content response on success, or 404 Not Found if missing.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var deleted = await _orderRepository.DeleteOrderAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Retrieves the dashboard summary for order metrics.
    /// </summary>
    /// <returns>A 200 OK response containing the dashboard summary.</returns>
    [HttpGet("dashboard-summary")]
    public async Task<IActionResult> GetDashboardSummary()
    {
        var summary = await _orderRepository.GetDashboardSummaryAsync();
        return Ok(summary);
    }

    /// <summary>
    /// Represents a request payload for updating an order status.
    /// </summary>
    public class UpdateOrderStatusRequest
    {
        /// <summary>
        /// Gets or sets the new status for the order.
        /// </summary>
        [Required]
        public OrderStatus Status { get; set; }
    }
}
