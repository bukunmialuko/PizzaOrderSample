using GraphQL.Types;
using PizzaOrder.Business.Models;
using PizzaOrder.GraphQLModels.EnumTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrder.GraphQLModels.EventTypes
{
    public class EventDataType : ObjectGraphType<EventDataModel>
    {
        public EventDataType()
        {
            Name = nameof(EventDataType);
            Field(x => x.OrderId);
        }
    }
}
