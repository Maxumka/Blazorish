using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish.Html;

public static class TagBuilder
{
    public static void Build(RenderTreeBuilder builder, Tag tag, object receiver)
    {
        var seq = -1;
        
        BuildNode(builder, ref seq, tag, receiver);
    }

    private static void BuildNode(RenderTreeBuilder builder, ref int seq, Tag tag, object receiver)
    {
        builder.OpenElement(seq++, tag.Name);
        
        foreach (var attr in tag.Attrs.Where(a => a is AttrClasses))
        {
            var classes = (attr as AttrClasses)!.Classes;

            foreach (var @class in classes)
            {
                builder.AddAttribute(seq++, "class", @class);
            }
        }

        foreach (var attr in tag.Attrs.Where(a => a is AttrOnClick))
        {
            var action = (attr as AttrOnClick)!.Action;
            builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(receiver, action));
        }
        
        foreach (var attr in tag.Attrs.Where(a => a is AttrId))
        {
            var id = (attr as AttrId)!.Id;
            builder.AddAttribute(seq++, "id", id);
        }
        
        foreach (var attr in tag.Attrs)
        {
            if (attr is AttrContent {Content: var content})
            {
                builder.AddContent(seq++, content);
            }
            
            if (attr is AttrChildren {Children: var children})
            {
                foreach (var child in children)
                {
                    BuildNode(builder, ref seq, child, receiver);
                }
            }
        }
        
        builder.CloseElement();
    }
}