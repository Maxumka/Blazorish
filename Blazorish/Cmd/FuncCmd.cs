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
    public static OfFuncEither<msg> OfFuncEither<sucSubMsg, errSubMsg, res>(
        Func<res> func,
        Func<res, sucSubMsg> suc,
        Func<Exception, errSubMsg> err) 
        where sucSubMsg : msg
        where errSubMsg : msg
    {
        var sucFunc = () => suc(func());
        var sucMsgFunc = sucFunc
            .AsFunc<sucSubMsg, msg>();
        
        var errMsgFunc = err
            .AsFunc<errSubMsg, msg>();
        
        return new OfFuncEither<msg>(sucMsgFunc, errMsgFunc);
    }

    public static OfFuncPerform<msg> OfFuncPerform<subMsg, res>(Func<res> func, Func<res, subMsg> suc) 
        where subMsg : msg
    {
        var sucMsgFunc = () => suc(func());

        return new OfFuncPerform<msg>(sucMsgFunc.AsFunc<subMsg, msg>());
    }
}