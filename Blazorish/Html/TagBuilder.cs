using Blazorish.Html.Elements;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish.Html;

public static class TagBuilder
{
    public static void Build(RenderTreeBuilder builder, Element element, object receiver)
    {
        var seq = -1;
        
        BuildNode(builder, ref seq, element, receiver);
    }

    public static void BuildNode(RenderTreeBuilder builder, ref int seq, Element element, object receiver)
    {
        element.Build(builder, ref seq, receiver);

        if (element is not Tag tag)
        {
            return;
        }
        
        foreach (var child in tag.Children)
        {
            BuildNode(builder, ref seq, child, receiver);
        }
            
        builder.CloseElement();
    }
}