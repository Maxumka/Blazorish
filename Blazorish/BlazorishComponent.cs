using Blazorish.Cmd;
using Blazorish.Html;
using static System.Console;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish;

public abstract class BlazorishComponent<TModel, TMsg> : ComponentBase
    where TMsg : class 
{
    private TModel _innerModel;
    
    private TModel InnerModel
    {
        get => _innerModel;
        set
        {
            _innerModel = value;
            StateHasChanged();
        }
    }

    protected abstract (TModel, Cmd<TMsg>) Init();
    
    protected abstract (TModel, Cmd<TMsg>) Update(TModel model, TMsg msg);

    protected void Dispatch(TMsg msg)
    {
        (InnerModel, var cmd) = Update(InnerModel, msg);
        
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
            case OfTaskPerform<TMsg> asyncPerform:
                InvokeAsync(() => asyncPerform.Dispatch(Dispatch));
                break;
            case OfTaskEither<TMsg> asyncEither:
                InvokeAsync(() => asyncEither.Dispatch(Dispatch));
                break;
        }
    }
    
    protected override void OnInitialized()
    {
        (InnerModel, var cmd) = Init();
        
        HandleCmd(cmd);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        WriteLine(_innerModel);
        
        base.OnAfterRender(firstRender);
    }
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        TagBuilder.Build(builder, View(InnerModel), this);
        
        base.BuildRenderTree(builder);
    }
}