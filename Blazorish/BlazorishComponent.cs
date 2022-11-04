using Blazorish.Cmd;
using Blazorish.Html;
using static System.Console;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish;

public abstract class BlazorishComponent<TModel, TMsg> : ComponentBase
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

    protected abstract (TModel, Cmd<TMsg>) Init();
    
    protected abstract (TModel, Cmd<TMsg>) Update(TModel model, TMsg msg);

    protected void Dispatch(TMsg msg)
    {
        (Model, var cmd) = Update(Model, msg);
        
        HandleCmd(cmd);
    }
    
    protected abstract Tag View(TModel model);

    private void HandleCmd(Cmd<TMsg> cmd)
    {
        switch (cmd)
        {
            case None<TMsg>:
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
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        TagBuilder.Build(builder, View(Model), this);
        
        base.BuildRenderTree(builder);
    }
}