namespace Blazorish.Extensions;

public static class FuncExtension
{
    public static Func<Exception, T> AsFunc<TDerived, T>(this Func<Exception, TDerived> func)
        where TDerived : T
        where T : class 
        => ex => func(ex);
}