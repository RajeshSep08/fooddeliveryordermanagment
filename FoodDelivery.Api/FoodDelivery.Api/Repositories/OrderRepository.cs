using FoodDelivery.Api.Data;
using FoodDelivery.Api.Interfaces;
using FoodDelivery.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.Repositories;

/// <summary>
/// Provides Entity Framework Core-based persistence for orders.
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly FoodDeliveryDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderRepository"/> class.
    /// </summary>
    /// <param name="context">The database context used for persistence.</param>
    public OrderRepository(FoodDeliveryDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Order>> SearchOrdersAsync(string? customerName, OrderStatus? status)
    {
        var query = _context.Orders.AsQueryable();

        if (!string.IsNullOrWhiteSpace(customerName))
        {
            var normalizedName = customerName.Trim();
            query = query.Where(o => o.CustomerName.ToLower().Contains(normalizedName.ToLower()));
        }

        if (status.HasValue)
        {
            query = query.Where(o => o.Status == status.Value);
        }

        return await query
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Order> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateOrderAsync(Order order)
    {
        var existingOrder = await _context.Orders.FindAsync(order.Id);
        if (existingOrder is null)
        {
            return false;
        }

        existingOrder.CustomerName = order.CustomerName;
        existingOrder.CustomerPhone = order.CustomerPhone;
        existingOrder.FoodItem = order.FoodItem;
        existingOrder.Quantity = order.Quantity;
        existingOrder.Price = order.Price;
        existingOrder.DeliveryAddress = order.DeliveryAddress;
        existingOrder.Status = order.Status;
        existingOrder.OrderDate = order.OrderDate;

        await _context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateOrderStatusAsync(int id, OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
        {
            return false;
        }

        order.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
    {
        var orders = await _context.Orders.ToListAsync();

        return new DashboardSummaryDto
        {
            TotalOrders = orders.Count,
            PlacedOrders = orders.Count(o => o.Status == OrderStatus.Placed),
            PreparingOrders = orders.Count(o => o.Status == OrderStatus.Preparing),
            OutForDeliveryOrders = orders.Count(o => o.Status == OrderStatus.OutForDelivery),
            DeliveredOrders = orders.Count(o => o.Status == OrderStatus.Delivered),
            CancelledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled),
            TotalRevenue = orders.Sum(o => o.Price)
        };
    }
}
