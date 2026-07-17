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
    /// <returns>A list of orders.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Retrieves a single order by identifier.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <returns>The matching order or not found.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// Searches for orders by customer name and/or status.
    /// </summary>
    /// <param name="customerName">The customer name to search for.</param>
    /// <param name="status">Optional order status filter.</param>
    /// <returns>A filtered list of orders.</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchOrders([FromQuery] string? customerName, [FromQuery] OrderStatus? status)
    {
        var orders = await _orderRepository.SearchOrdersAsync(customerName, status);
        return Ok(orders);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The order details to create.</param>
    /// <returns>The created order.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        await _orderRepository.AddOrderAsync(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <param name="order">The updated order details.</param>
    /// <returns>No content when the update succeeds.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (id != order.Id)
        {
            return BadRequest();
        }

        var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
        if (existingOrder is null)
        {
            return NotFound();
        }

        await _orderRepository.UpdateOrderAsync(order);
        return NoContent();
    }

    /// <summary>
    /// Updates the status of an existing order.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <param name="request">The new status value.</param>
    /// <returns>No content when the status update succeeds.</returns>
    [HttpPatch("{id:int}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
        if (existingOrder is null)
        {
            return NotFound();
        }

        await _orderRepository.UpdateOrderStatusAsync(id, request.Status);
        return NoContent();
    }

    /// <summary>
    /// Deletes an order by identifier.
    /// </summary>
    /// <param name="id">The order identifier.</param>
    /// <returns>No content when the delete succeeds.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
        if (existingOrder is null)
        {
            return NotFound();
        }

        await _orderRepository.DeleteOrderAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Retrieves dashboard summary information for orders.
    /// </summary>
    /// <returns>A summary object with order counts and revenue.</returns>
    [HttpGet("dashboard-summary")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboardSummary()
    {
        var summary = await _orderRepository.GetDashboardSummaryAsync();
        return Ok(summary);
    }

    /// <summary>
    /// Represents a request payload for updating the order status.
    /// </summary>
    public class UpdateOrderStatusRequest
    {
        /// <summary>
        /// Gets or sets the new order status.
        /// </summary>
        [Required]
        public OrderStatus Status { get; set; }
    }
}
