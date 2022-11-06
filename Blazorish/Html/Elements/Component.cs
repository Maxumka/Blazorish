using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish.Html.Elements;

public sealed class Component<T> : Element where T : ComponentBase
{
    private readonly Type _componentType;

    private readonly (string, object)[] _attributes;
    
    private Component((string, object)[] attributes)
    {
        _componentType = typeof(T);
        _attributes = attributes;
    }

    internal override void Build(RenderTreeBuilder builder, ref int seq, object receiver)
    {
        builder.OpenComponent(seq++, _componentType);
        
        foreach (var (name, value) in _attributes)
        {
            if (name is "ChildContent" && value is Element element)
            {
                var innerSeq = seq++;
                builder.AddAttribute(seq++, name, (RenderFragment)(innerBuilder =>
                {
                    TagBuilder.BuildNode(innerBuilder, ref innerSeq, element, receiver);
                }));
                seq = innerSeq;
            }
            else
            {
                builder.AddAttribute(seq++, name, value);
            }
        }
        
        builder.CloseComponent();
    }
    
    public static Component<T> create(params (string, object)[] attrs) 
        => new(attrs);
}