using System;

public static class FunctionalExtension
{
    static Func<object> BuildMethod<T>(Func<T> func)
    {
        return () => func();
    }
}