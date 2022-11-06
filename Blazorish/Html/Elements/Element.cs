using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish.Html.Elements;

public abstract class Element
{
    internal abstract void Build(RenderTreeBuilder builder, ref int seq, object receiver);
}