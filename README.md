# PizzaOrderSample 


## Query

mutation($orderDetails: OrderDetailsInputType!) {
  createOrder(orderDetails: $orderDetails) {
    id
    orderStatus
    pizzaDetails {
      id
      name
    }
  }
}



## Params

{
  "orderDetails": {
    "addressLine1": "Wulemotu Street",
    "addressLine2": "Ayobo",
    "mobileNo": "7033761964",
    "amount": 150,
    "pizzaDetails": [{
      "name": "Eba",
      "price": 200,
      "size": 10,
      "toppings": "PEPPERONI"
    }, 
    {
      "name": "Amala",
      "price": 250,
      "size": 12,
      "toppings": "MUSHROOMS"
      
    }]
  }
}