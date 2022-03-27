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

public abstract partial record Cmd<TMsg> where TMsg : class
{
    public static None<TMsg> None()
        => new None<TMsg>();

    public static OfMsg<TMsg> OfMsg(TMsg msg)
        => new OfMsg<TMsg>(msg);
}   