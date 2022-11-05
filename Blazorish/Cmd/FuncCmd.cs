using Blazorish.Extensions;

namespace Blazorish.Cmd;

public sealed record OfFuncEither<TMsg>(Func<TMsg> Suc, Func<Exception, TMsg> Err) : Cmd<TMsg>
    where TMsg : class
{
    public override void Dispatch(Action<TMsg> dispatch)
    {
        try
        {
            var suc = Suc();
            dispatch(suc);
        }
        catch(Exception e)
        {
            var err = Err(e);
            dispatch(err);
        }
    }
}

public sealed record OfFuncPerform<TMsg>(Func<TMsg> Suc) : Cmd<TMsg>
    where TMsg : class
{
    public override void Dispatch(Action<TMsg> dispatch)
    {
        var suc = Suc();
        dispatch(suc);
    }
}

public abstract partial record Cmd<msg> where msg : class
{
    public static OfFuncEither<msg> OfFuncEither<TMsgDerived, TA, TB>(
        Func<TA, TB> func,
        TA arg,
        Func<TB, TMsgDerived> suc,
        Func<Exception, TMsgDerived> err
    ) where TMsgDerived : msg
    {
        var sucMsgFunc = () => suc(func(arg));

        return new OfFuncEither<msg>(sucMsgFunc.AsFunc<TMsgDerived, msg>(), err.AsFunc<TMsgDerived, msg>());
    }

    public static OfFuncPerform<msg> OfFuncPerform<TMsgDerived, TA, TB>(
        Func<TA, TB> func,
        TA arg,
        Func<TB, TMsgDerived> suc
    ) where TMsgDerived : msg
    {
        var sucMsgFunc = () => suc(func(arg));

        return new OfFuncPerform<msg>(sucMsgFunc.AsFunc<TMsgDerived, msg>());
    }
}