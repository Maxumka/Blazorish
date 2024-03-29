﻿using Blazorish.Html;
using Blazorish.Html.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish;

public abstract class BlazorishComponentOnlyHtml : ComponentBase
{
    protected abstract Element View();
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        TagBuilder.Build(builder, View(), this);
        
        base.BuildRenderTree(builder);
    }
}