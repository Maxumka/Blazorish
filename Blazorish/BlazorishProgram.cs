using static System.Console;
using Microsoft.AspNetCore.Components;

namespace Blazorish;

public abstract class BlazorProgram<TModel, TMsg> : ComponentBase
    where TMsg : class 
{
    private TModel _model;

    private bool _reRender = true;
    
    protected TModel Model
    {
        get => _model;
        private set
        {
            _model = value;
            StateHasChanged();
        }
    }

    protected abstract (TModel, Cmd<TMsg>) Init();
    
    protected abstract (TModel, Cmd<TMsg>) Update(TModel model, TMsg msg);

    protected void Dispatch(TMsg msg)
    {
        (Model, var cmd) = Update(Model, msg);

        if (!_reRender)
        {
            HandleCmd(Cmd<TMsg>.None());
            return;
        }
        
        HandleCmd(cmd);
    }
    
    private void HandleCmd(Cmd<TMsg> cmd)
    {
        switch (cmd)
        {
            case None<TMsg>:
                _reRender = !_reRender;
                break;
            case OfMsg<TMsg> cmdMsg:
                Dispatch(cmdMsg.Msg);
                break;
            case OfAsync<TMsg> cmdAsync:
                InvokeAsync(() => cmdAsync.Task);
                break;
            case OfAsyncPerform<TMsg> cmdAsync:
                InvokeAsync(() => HandleCmdOfAsyncPerform(cmdAsync.Msg));
                break;
            case OfAsyncEither<TMsg> either:
                InvokeAsync(() => HandleCmdOfAsyncEither(either));
                break;
        }
    }

    private async Task HandleCmdOfAsyncPerform(Task<TMsg> task)
    {
        var msg = await task;
        
        Dispatch(msg);
    }

    private async Task HandleCmdOfAsyncEither(OfAsyncEither<TMsg> either)
    {
        try
        {
            var suc = await either.Suc;

            Dispatch(suc);
        }
        catch(Exception e)
        {
            var err = either.Err(e);
            
            Dispatch(err);
        }
    }
    
    protected override void OnInitialized()
    {
        (Model, var cmd) = Init();
        
        HandleCmd(cmd);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        WriteLine(_model);
        
        base.OnAfterRender(firstRender);
    }
}