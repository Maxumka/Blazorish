using Blazorish.Html;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish;

public abstract class BlazorishSimpleComponent<TModel, TMsg> : ComponentBase
{
    private TModel _model;

    private TModel Model
    {
        get => _model;
        set
        {
            _model = value;
            StateHasChanged();
        }
    }

    protected abstract TModel Init();
    
    protected abstract TModel Update(TModel model, TMsg msg);
    
    protected void Dispatch(TMsg msg)
    {
        Model = Update(Model, msg);
    }
    
    protected abstract Tag View(TModel model);
    
    protected override void OnInitialized()
    {
        Model = Init();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        Console.WriteLine(_model);
        
        base.OnAfterRender(firstRender);
    }
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        TagBuilder.Build(builder, View(Model), this);
        
        base.BuildRenderTree(builder);
    }
}