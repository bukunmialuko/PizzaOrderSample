using Microsoft.EntityFrameworkCore;
using PizzaOrder.Business.Models;
using PizzaOrder.Data;
using PizzaOrder.Data.Entities;
using PizzaOrder.Data.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder.Business.Services
{
    public interface IOrderDetailsService
    {
        Task<OrderDetails> CreateAsync(OrderDetails orderDetails);
        Task<OrderDetails> GetOrderDetailsAsync(int orderId);
        Task<IEnumerable<OrderDetails>> GettAllNewOrdersAsync();
        Task<OrderDetails> UpdateStatusAsync(int orderId, OrderStatus orderStatus);
    }

    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly PizzaDBContext _dbContext;

        private readonly IEventService _eventService;

        public OrderDetailsService(PizzaDBContext dbContext, IEventService eventService)
        {
            _dbContext = dbContext;
            _eventService = eventService;
        }

        public async Task<IEnumerable<OrderDetails>> GettAllNewOrdersAsync()
        {
            return await _dbContext.OrderDetails
                .Where(x => x.OrderStatus == Data.Enums.OrderStatus.Created)
                .ToListAsync();
        }

        public async Task<OrderDetails> GetOrderDetailsAsync(int orderId)
        {
            return await _dbContext.OrderDetails.FindAsync(orderId);
        }

        public async Task<OrderDetails> CreateAsync(OrderDetails orderDetails)
        {
            _dbContext.OrderDetails.Add(orderDetails);
            await _dbContext.SaveChangesAsync();
            _eventService.CreateOrderEvent(new EventDataModel(38));
            return orderDetails;
        }


        public async Task<OrderDetails> UpdateStatusAsync(int orderId, OrderStatus orderStatus)
        {
            var orderDetails = await _dbContext.OrderDetails.FindAsync(orderId);

            if (orderDetails != null)
            {
                orderDetails.OrderStatus = orderStatus;
                await _dbContext.SaveChangesAsync();
         }

            return orderDetails;
        }

    }

}