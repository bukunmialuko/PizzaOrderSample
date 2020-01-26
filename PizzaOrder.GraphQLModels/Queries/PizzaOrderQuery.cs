using GraphQL.Types;
using PizzaOrder.Business.Services;
using PizzaOrder.GraphQLModels.EntityTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrder.GraphQLModels.Queries
{
    public class PizzaOrderQuery : ObjectGraphType
    {
        public PizzaOrderQuery(IOrderDetailsService orderDetailsService, IPizzaDetailsService pizzaDetailsService)
        {
            Name = nameof(PizzaOrderQuery);
            //this.AuthorizeWith(Constants.AuthPolicy.CustomerPolicy, Constants.AuthPolicy.RestaurantPolicy);

            FieldAsync<ListGraphType<OrderDetailsType>>(
                name: "newOrders",
                resolve: async context => await orderDetailsService.GettAllNewOrdersAsync());
            //.AuthorizeWith(Constants.AuthPolicy.RestaurantPolicy);


            FieldAsync<PizzaDetailsType>(
             name: "pizzaDetails",
             arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
             resolve: async context => await pizzaDetailsService.GetPizzaDetailsAsync(context.GetArgument<int>("id")));



            FieldAsync<OrderDetailsType>(
                name: "orderDetails",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: async context => await orderDetailsService.GetOrderDetailsAsync(context.GetArgument<int>("id")));




        }
    }
}