namespace Blazorish.Cmd;

public sealed record None<TMsg> : Cmd<TMsg>
    where TMsg : class
{
    public override void Dispatch(Action<TMsg> dispatch) {}
}

public sealed record OfMsg<TMsg>(TMsg Msg) : Cmd<TMsg>
    where TMsg : class
{
    public override void Dispatch(Action<TMsg> dispatch)
    {
        dispatch(Msg);
    }
}

public abstract partial record Cmd<msg> where msg : class
{
    public static None<msg> None()
        => new None<msg>();

    public static OfMsg<msg> OfMsg(msg msg)
        => new OfMsg<msg>(msg);
}   