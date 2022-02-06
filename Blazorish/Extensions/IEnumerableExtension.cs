namespace Blazorish.Extensions;

public static class IEnumerableExtension
{
    public static void Print<T>(this IEnumerable<T> xs, Func<T, string>? func = null)
        where T : notnull
    {
        Console.WriteLine("[");
        
        foreach (var x in xs)
        {
            var line = func?.Invoke(x) ?? x.ToString();
            
            Console.WriteLine(line);
        }
        
        Console.WriteLine("]");
    }
}