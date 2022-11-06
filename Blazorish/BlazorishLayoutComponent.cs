using Blazorish.Html;
using Blazorish.Html.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish;

public class BlazorishLayoutComponent : LayoutComponentBase
{
    protected virtual Element View() => 
        Fragment.create(Body);
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        TagBuilder.Build(builder, View(), this);
        
        base.BuildRenderTree(builder);
    }
}