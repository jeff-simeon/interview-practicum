﻿using System.Collections.Generic;
using System.Linq;
using Application;
using NUnit.Framework;

namespace ApplicationTests;

[TestFixture]
public class DishManagerTests
{
    [SetUp]
    public void Setup()
    {
        _sut = new DishManager();
    }

    private DishManager _sut;

    [Test]
    public void EmptyListReturnsEmptyList()
    {
        var order = new Order();
        var actual = _sut.GetDishes(order).Result;
        Assert.AreEqual(0, actual.Count);
    }

    [Test]
    public void ListWith1ReturnsOneSteak()
    {
        var order = new Order
        {
            Dishes = new List<int>
            {
                1
            }
        };

        var actual = _sut.GetDishes(order).Result;
        Assert.AreEqual(1, actual.Count);
        Assert.AreEqual("steak", actual.First().DishName);
        Assert.AreEqual(1, actual.First().Count);
    }
}