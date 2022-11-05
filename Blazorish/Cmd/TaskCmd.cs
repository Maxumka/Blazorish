using Blazorish.Extensions;

namespace Blazorish.Cmd;

public sealed record OfTaskEither<TMsg>(Task<TMsg> Suc, Func<Exception, TMsg> Err) : Cmd<TMsg>
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

public sealed record OfTaskPerform<TMsg>(Task<TMsg> Suc) : Cmd<TMsg>
    where TMsg : class
{
    public override async void Dispatch(Action<TMsg> dispatch)
    {
        var suc = await Suc;

        dispatch(suc);
    }
}

public abstract partial record Cmd<msg> where msg : class
{
    public static OfTaskEither<msg> OfTaskEither<sucSubMsg, errSubMsg, res>(
        Task<res> task,
        Func<res, sucSubMsg> sucFunc,
        Func<Exception, errSubMsg> errFunc)
        where sucSubMsg : msg
        where errSubMsg : msg
    {
        var sucMsgTask = Task
            .Run(async () => sucFunc(await task))
            .AsTask<sucSubMsg, msg>();

        var errMsg = errFunc
            .AsFunc<errSubMsg, msg>();

        return new OfTaskEither<msg>(sucMsgTask, errMsg);
    }
    
    public static OfTaskPerform<msg> OfTaskPerform<subMsg, res>(Task<res> task, Func<res, subMsg> msg)
        where subMsg : msg
    {
        var msgTask = Task
            .Run(async () => msg(await task))
            .AsTask<subMsg, msg>();

        return new OfTaskPerform<msg>(msgTask);
    }
}