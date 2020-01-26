using PizzaOrder.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrder.Business.Models
{
    public class EventDataModel
    {
        public int OrderId { get; set; }

        public EventDataModel(int orderId)
        {
            OrderId = orderId;
        }
    }
}
