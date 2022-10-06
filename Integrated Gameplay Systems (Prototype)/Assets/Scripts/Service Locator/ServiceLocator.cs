using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, object> objects = new Dictionary<Type, object>();

    public static void RegisterService<T> (T t)
    {
        objects.Add(typeof(T), t);
    }

    public static T GetService<T> ()
    {
        return (T) objects[typeof(T)];
    }
}
