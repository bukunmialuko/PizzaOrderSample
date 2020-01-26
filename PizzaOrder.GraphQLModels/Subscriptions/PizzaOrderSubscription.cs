using GraphQL.Resolvers;
using GraphQL.Types;
using PizzaOrder.Business.Models;
using PizzaOrder.Business.Services;
using PizzaOrder.Data.Enums;
using PizzaOrder.GraphQLModels.EventTypes;

namespace PizzaOrder.GraphQLModels.Subscriptions
{
    public class PizzaOrderSubscription : ObjectGraphType
    {
        private readonly IEventService _eventService;

        public PizzaOrderSubscription(IEventService eventService)
        {
            _eventService = eventService;
            Name = nameof(PizzaOrderSubscription);

            AddField(new EventStreamFieldType
            {
                Name = "orderCreated",
                Type = typeof(EventDataType),
                Resolver = new FuncFieldResolver<EventDataModel>(context => context.Source as EventDataModel),
                Subscriber = new EventStreamResolver<EventDataModel>(context =>
                {
                    return _eventService.OnCreateObservable;
                })
            });

 
        }
    }
}
