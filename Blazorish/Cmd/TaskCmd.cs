using Blazorish.Extensions;

namespace Blazorish.Cmd;

public sealed record OfAsyncEither<TMsg>(Task<TMsg> Suc, Func<Exception, TMsg> Err) : Cmd<TMsg>
    where TMsg : class
{
    public override async void Dispatch(Action<TMsg> dispatch)
    {
        try
        {
            var suc = await Suc;

            dispatch(suc);
        }
        catch(Exception e)
        {
            var err = Err(e);
            
            dispatch(err);
        }
    }
}

public sealed record OfAsyncPerform<TMsg>(Task<TMsg> Suc) : Cmd<TMsg>
    where TMsg : class
{
    public override async void Dispatch(Action<TMsg> dispatch)
    {
        var suc = await Suc;

        dispatch(suc);
    }
}

public abstract partial record Cmd<TMsg> where TMsg : class
{
    public static OfAsyncEither<TMsg> OfAsyncEither<TMsgSucDerived, TMsgErrDerived, TA, TB>(
        Func<TA, Task<TB>> func,
        TA arg,
        Func<TB, TMsgSucDerived> suc,
        Func<Exception, TMsgErrDerived> err)
        where TMsgSucDerived : TMsg
        where TMsgErrDerived : TMsg
    {
        var sucMsgTask = Task.Run(async () => suc(await func(arg)))
            .AsTask<TMsgSucDerived, TMsg>();

        var errMsg = err.AsFunc<TMsgErrDerived, TMsg>();

        return new OfAsyncEither<TMsg>(sucMsgTask, errMsg);
    }
    
    public static OfAsyncPerform<TMsg> OfAsyncPerform<TMsgDerived, TA, TB>(Func<TA, Task<TB>> func, TA arg, Func<TB, TMsgDerived> msg)
        where TMsgDerived : TMsg
    {
        var msgTask = Task.Run(async () => msg(await func(arg)))
            .AsTask<TMsgDerived, TMsg>();

        return new OfAsyncPerform<TMsg>(msgTask);
    }
}