using System;
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
        return Task.FromResult(order.GetDishes());
    }
}