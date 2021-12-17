using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtension // None of these methods are my code
{
    public static T PickRandom<T>(this IEnumerable<T> source) // Pick random from list
    {
        return source.PickRandom(1).Single();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count) // Pick N randoms from list
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) // Shuffle order
    {
        return source.OrderBy(x => Guid.NewGuid());
    }

    public static T RandomElementByWeight<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector) // Pick a random element based on its weight
    {
        var totalWeight = sequence.Sum(weightSelector);
        var itemWeightIndex = (float)new Random().NextDouble() * totalWeight;
        float currentWeightIndex = 0;

        foreach (var item in from weightedItem in sequence
            select new { Value = weightedItem, Weight = weightSelector(weightedItem) })
        {
            currentWeightIndex += item.Weight;

            if (currentWeightIndex >= itemWeightIndex)
                return item.Value;
        }

        return default;
    }
}