using Blazorish.Html;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish;

public abstract class BlazorishComponentOnlyHtml : ComponentBase
{
    protected abstract Tag View();

    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        TagBuilder.Build(builder, View(), this);
        
        base.BuildRenderTree(builder);
    }
}