using Blazorish.Extensions;

namespace Blazorish;

public abstract record Cmd<TMsg> where TMsg : class
{
    public static None<TMsg> None() => 
        new();

    public static OfAsync<TMsg> OfAsync<TMsgDerived, T, U>(Func<T, Task<U>> func, T arg, Func<U, TMsgDerived> msg)
        where TMsgDerived : TMsg
    {
        var msgTask = Task.Run(async () => msg(await func(arg)))
            .AsTask<TMsgDerived, TMsg>();

        return new(msgTask);
    }
}

public sealed record None<TMsg> : Cmd<TMsg> 
    where TMsg : class;

public sealed record OfAsync<TMsg>(Task<TMsg> Msg) : Cmd<TMsg> 
    where TMsg : class;