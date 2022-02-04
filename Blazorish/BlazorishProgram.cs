using Microsoft.AspNetCore.Components;

namespace Blazorish;

public abstract class BlazorishProgram<TModel, TMsg> : ComponentBase
    where TMsg : class 
{
    private TModel _model;

    protected TModel Model
    {
        get => _model;
        set
        {
            _model = value;
            StateHasChanged();
        }
    }
    
    protected abstract (TModel, Cmd<TMsg>) Update(TModel model, TMsg msg);

    protected void Dispatch(TMsg msg)
    {
        (Model, var cmd) = Update(Model, msg);
        
        HandleCmd(cmd);
    }
    
    private void HandleCmd(Cmd<TMsg> cmd)
    {
        async Task HandleCmdOfAsync(Task<TMsg> task)
        {
            var msg = await task;
        
            Dispatch(msg);
        }
        
        switch (cmd)
        {
            case None<TMsg>:
                return;
            case OfAsync<TMsg> cmdAsync:
                InvokeAsync(() => HandleCmdOfAsync(cmdAsync.Msg));
                break;
        }
    }
}