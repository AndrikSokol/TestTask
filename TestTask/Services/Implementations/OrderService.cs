using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            var orderWithMaxAmount = await (from order in _context.Orders
                                            where (order.Price * order.Quantity) == _context.Orders.Max(o => o.Price * o.Quantity)
                                            select order)
                                    .FirstAsync();

            return orderWithMaxAmount;
        }

        public async Task<List<Order>> GetOrders()
        {
            List<Order> orders = await (from order in _context.Orders
                                        where order.Quantity > 10
                                        select order)
                                          .ToListAsync();

            return orders;
        }
    }
}
