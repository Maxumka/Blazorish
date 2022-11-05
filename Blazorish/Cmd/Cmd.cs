namespace Blazorish.Cmd;

public abstract partial record Cmd<msg> where msg : class
{
    public abstract void Dispatch(Action<msg> dispatch);
}