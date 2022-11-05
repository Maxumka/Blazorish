using Blazorish.Html;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish;

public abstract class BlazorishSimpleComponent<TModel, TMsg> : ComponentBase
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

    protected abstract TModel Init();
    
    protected abstract TModel Update(TModel model, TMsg msg);
    
    protected void Dispatch(TMsg msg)
    {
        InnerModel = Update(InnerModel, msg);
    }
    
    protected abstract Tag View(TModel model);
    
    protected override void OnInitialized()
    {
        InnerModel = Init();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        Console.WriteLine(_innerModel);
        
        base.OnAfterRender(firstRender);
    }
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        TagBuilder.Build(builder, View(InnerModel), this);
        
        base.BuildRenderTree(builder);
    }
}