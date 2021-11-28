using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectExtension
{
    public static T GetInterface<T>(this GameObject inObj) where T : class
    {
        if (!typeof(T).IsInterface) {
            var typeString = typeof(T).ToString();
            throw new NotSupportedException($"Supplied generic is invalid, must be Interface.\nSupplied: {typeString}");
        }
 
        return inObj.GetComponents<Component>().OfType<T>().FirstOrDefault();
    }
 
    public static IEnumerable<T> GetInterfaces<T>(this GameObject inObj) where T : class
    {
        if (!typeof(T).IsInterface) {
            var typeString = typeof(T).ToString();
            throw new NotSupportedException($"Supplied generic is invalid, must be Interface.\nSupplied: {typeString}");
        }
 
        return inObj.GetComponents<Component>().OfType<T>();
    }
}