using GraphQL;
using GraphQL.Server;
using Microsoft.Extensions.DependencyInjection;
using PizzaOrder.Business.Services;
using PizzaOrder.GraphQLModels.EntityTypes;
using PizzaOrder.GraphQLModels.EnumTypes;
using PizzaOrder.GraphQLModels.EventTypes;
using PizzaOrder.GraphQLModels.InputTypes;
using PizzaOrder.GraphQLModels.Mutations;
using PizzaOrder.GraphQLModels.Queries;
using PizzaOrder.GraphQLModels.Schema;
using PizzaOrder.GraphQLModels.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder.Api
{
    public static class ConfigureServiceExtension
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddTransient<IPizzaDetailsService, PizzaDetailsService>();
            services.AddTransient<IOrderDetailsService, OrderDetailsService>();
            services.AddTransient<IEventService, EventService>();
        }

        public static void AddCustomGraphQLServices(this IServiceCollection services)
        {
            // GraphQL services
            services.AddScoped<IServiceProvider>(c => new FuncServiceProvider(type => c.GetRequiredService(type)));
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = true; // false prints message only, true will ToString
                options.UnhandledExceptionDelegate = context =>
                {
                    Console.WriteLine("Error: " + context.OriginalException.Message);
                };
            })
            .AddWebSockets()
            .AddDataLoader()
            .AddGraphTypes(typeof(PizzaOrderSchema));
        }


        public static void AddCustomGraphQLTypes(this IServiceCollection services)
        {
            services.AddSingleton<OrderDetailsType>();
            services.AddSingleton<PizzaDetailsType>();
            services.AddSingleton<EventDataType>();

            services.AddSingleton<OrderStatusEnumType>();
            services.AddSingleton<ToppingsEnumType>();


            services.AddSingleton<OrderDetailsInputType>();
            services.AddSingleton<PizzaDetailsInputType>();

            services.AddSingleton<PizzaOrderQuery>();
            services.AddSingleton<PizzaOrderSchema>();
            services.AddSingleton<PizzaOrderMutation>();
            services.AddSingleton<PizzaOrderSubscription>();


        }
    }
}
