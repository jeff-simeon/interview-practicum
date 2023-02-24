using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application;

public abstract class Order
{
    public Order()
    {
        Dishes = new List<int>();
    }
    public abstract Task<List<Dish>> GetDishes();
    protected abstract bool IsMultipleAllowed(int order);
    protected abstract string GetOrderName(int order);

    protected void AddOrderToList(int order, List<Dish> returnValue)
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

    public List<int> Dishes { get; set; }
}

public class MorningOrder:Order{
    public MorningOrder()
    {

    }
    public override Task<List<Dish>> GetDishes()
    {
        var returnValue = new List<Dish>();
        Dishes.Sort();
        foreach (var dishType in Dishes) AddOrderToList(dishType, returnValue);

        return Task.FromResult(returnValue);
    }
    protected override string GetOrderName(int order)
    {
        switch (order)
        {
            case 1:
                return "egg";
            case 2:
                return "toast";
            case 3:
                return "coffee";
            default:
                throw new ApplicationException("Order does not exist");
        }
    }

    protected override bool IsMultipleAllowed(int order)
    {
        switch (order)
        {
            case 3:
                return true;
            default:
                return false;
        }
    }
}
public class EveningOrder:Order{
    public EveningOrder()
    {
        
    }
    public override Task<List<Dish>> GetDishes()
    {
        var returnValue = new List<Dish>();
        Dishes.Sort();
        foreach (var dishType in Dishes) AddOrderToList(dishType, returnValue);

        return Task.FromResult(returnValue);
    }
    protected override string GetOrderName(int order)
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
    protected override bool IsMultipleAllowed(int order)
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

public class OrderFactory
{
    public static Order CreateOrder(string mealType){
        switch (mealType)
        {
            case "morning":
                return new MorningOrder();
            case "evening":
                return new EveningOrder();
            default:
                throw new ApplicationException("Order does not exist");
        }
    }
}