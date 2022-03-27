using System.Runtime.CompilerServices;

namespace Blazorish.Extensions;

public static class FuncExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T> AsFunc<TDerived, T>(this Func<TDerived> func)
        where TDerived : T
        where T : class 
        => () => func();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<Exception, T> AsFunc<TDerived, T>(this Func<Exception, TDerived> func)
        where TDerived : T
        where T : class 
        => ex => func(ex);
}