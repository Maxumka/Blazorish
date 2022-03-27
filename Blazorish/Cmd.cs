using Blazorish.Extensions;

namespace Blazorish;

public abstract record Cmd<TMsg> where TMsg : class
{
    public static None<TMsg> None() => 
        new();

    public static OfMsg<TMsg> OfMsg(TMsg msg)
        => new OfMsg<TMsg>(msg);
    
    public static OfAsync<TMsg> OfAsync<T>(Func<T, Task> func, T arg)
    {
        return new OfAsync<TMsg>(func(arg));
    }

    public static OfAsyncPerform<TMsg> OfAsyncPerform<TMsgDerived, T, U>(Func<T, Task<U>> func, T arg, Func<U, TMsgDerived> msg)
        where TMsgDerived : TMsg
    {
        var msgTask = Task.Run(async () => msg(await func(arg)))
            .AsTask<TMsgDerived, TMsg>();

        return new(msgTask);
    }

    public static OfAsyncEither<TMsg> OfAsyncEither<TMsgDerived, T, U>(
        Func<T, Task<U>> func,
        T arg,
        Func<U, TMsgDerived> suc,
        Func<Exception, TMsgDerived> err)
        where TMsgDerived : TMsg
    {
        var sucMsgTask = Task.Run(async () => suc(await func(arg)))
            .AsTask<TMsgDerived, TMsg>();

        var errMsg = err.AsFunc<TMsgDerived, TMsg>();

        return new OfAsyncEither<TMsg>(sucMsgTask, errMsg);
    }
}

public sealed record None<TMsg> : Cmd<TMsg> 
    where TMsg : class;

public sealed record OfMsg<TMsg>(TMsg Msg) : Cmd<TMsg>
    where TMsg : class;

public sealed record OfAsync<TMsg>(Task Task) : Cmd<TMsg>
    where TMsg : class;

public sealed record OfAsyncPerform<TMsg>(Task<TMsg> Msg) : Cmd<TMsg> 
    where TMsg : class;

public sealed record OfAsyncEither<TMsg>(Task<TMsg> Suc, Func<Exception, TMsg> Err) : Cmd<TMsg>
    where TMsg : class;    