namespace Blazorish.Cmd;

public abstract partial record Cmd<TMsg> where TMsg : class
{
    public abstract void Dispatch(Action<TMsg> dispatch);
}