﻿using System;
using System.Collections.Generic;
using System.Linq;

public static class FunctionalExtension
{
    public static T PickRandomItemWeighted<T>(IList<(T Item, int Weight)> items)
    {
        if ((items?.Count ?? 0) == 0)
        {
            return default;
        }

        int offset = 0;
        (T Item, int RangeTo)[] rangedItems = items
            .OrderBy(item => item.Weight)
            .Select(entry => (entry.Item, RangeTo: offset += entry.Weight))
            .ToArray();

        int randomNumber = new Random().Next(items.Sum(item => item.Weight)) + 1;
        return rangedItems.First(item => randomNumber <= item.RangeTo).Item;
    }
}