using System.Reflection;

namespace Erpi.BuildingBlocks.Domain;

public abstract record ValueObject
{
    protected static IEnumerable<T> GetAllTypes<T>()
    {
        return typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.PropertyType == typeof(T))
            .Select(p => p.GetValue(null))
            .OfType<T>()
            .ToArray();
    }
}