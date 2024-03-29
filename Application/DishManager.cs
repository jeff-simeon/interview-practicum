﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application;

public class DishManager : IDishManager
{
  /// <summary>
  ///     Takes an Order object, sorts the orders and builds a list of dishes to be returned.
  /// </summary>
  /// <param name="order"></param>
  /// <returns></returns>
  public Task<List<Dish>> GetDishes(Order order)
    {
        var returnValue = new List<Dish>();
        order.Dishes.Sort();
        foreach (var dishType in order.Dishes) AddOrderToList(dishType, returnValue);

        return Task.FromResult(returnValue);
    }

  /// <summary>
  ///     Takes an int, representing an order type, tries to find it in the list.
  ///     If the dish type does not exist, add it and set count to 1
  ///     If the type exists, check if multiples are allowed and increment that instances count by one
  ///     else throw error
  /// </summary>
  /// <param name="order">int, represents a dishtype</param>
  /// <param name="returnValue">a list of dishes, - get appended to or changed </param>
  private void AddOrderToList(int order, List<Dish> returnValue)
    {
        var orderName = GetOrderName(order);
        var existingOrder = returnValue.SingleOrDefault(x => x.DishName == orderName);
        if (existingOrder == null)
            returnValue.Add(new Dish
            {
                DishName = orderName,
                Count = 1
            });
        else if (IsMultipleAllowed(order))
            existingOrder.Count++;
        else
            Console.WriteLine($"Multiple {orderName}(s) not allowed");
    }

    private string GetOrderName(int order)
    {
        switch (order)
        {
            case 1:
                return "steak";
            case 2:
                return "potato";
            case 3:
                return "wine";
            case 4:
                return "cake";
            default:
                throw new ApplicationException("Order does not exist");
        }
    }


    private bool IsMultipleAllowed(int order)
    {
        switch (order)
        {
            case 2:
                return true;
            default:
                return false;
        }
    }
}