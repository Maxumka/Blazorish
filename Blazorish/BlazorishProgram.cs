using Blazorish.Cmd;
using static System.Console;
using Microsoft.AspNetCore.Components;

namespace Blazorish;

public abstract class BlazorishProgram<TModel, TMsg> : ComponentBase
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
                cmdMsg.Dispatch(Dispatch);
                break;
            case OfFuncPerform<TMsg> funcPerform:
                funcPerform.Dispatch(Dispatch);
                break;
            case OfFuncEither<TMsg> funcEither:
                funcEither.Dispatch(Dispatch);
                break;
            case OfAsyncPerform<TMsg> asyncPerform:
                InvokeAsync(() => asyncPerform.Dispatch(Dispatch));
                break;
            case OfAsyncEither<TMsg> asyncEither:
                InvokeAsync(() => asyncEither.Dispatch(Dispatch));
                break;
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