using Microsoft.AspNetCore.Components;

namespace Blazorish;

public abstract class BlazorProgram<TModel, TMsg> : ComponentBase
    where TMsg : class 
{
    private TModel _model;

    protected TModel Model
    {
        get => _model;
        private set
        {
            _model = value;
            StateHasChanged();
        }
    }

    // callback for getting updated model from nested component
    [Parameter]
    public Action<TModel>? NestedDispatch { get; set; }
    
    protected abstract (TModel, Cmd<TMsg>) Init();
    
    protected abstract (TModel, Cmd<TMsg>) Update(TModel model, TMsg msg);

    protected void Dispatch(TMsg msg)
    {
        (Model, var cmd) = Update(Model, msg);
        
        HandleCmd(cmd);

        NestedDispatch?.Invoke(Model);
    }
    
    private async Task HandleCmdOfAsync(Task<TMsg> task)
    {
        var msg = await task;
        
        Dispatch(msg);
    }
    
    private void HandleCmd(Cmd<TMsg> cmd)
    {
        switch (cmd)
        {
            case None<TMsg>:
                return;
            case OfAsync<TMsg> cmdAsync:
                InvokeAsync(() => cmdAsync.Task);
                return;
            case OfAsyncResult<TMsg> cmdAsync:
                InvokeAsync(() => HandleCmdOfAsync(cmdAsync.Msg));
                break;
        }
    }

    protected override void OnInitialized()
    {
        (Model, var cmd) = Init();
        
        HandleCmd(cmd);
    }
}