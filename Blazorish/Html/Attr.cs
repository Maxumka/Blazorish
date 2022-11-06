using Blazorish.Html.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazorish.Html;

public sealed class AttrContent : Attr
{
    public string? Content { get; init; }
    
    internal AttrContent(string? content)
    {
        Content = content;
    }
}

public sealed class AttrId : Attr
{
    public string Id { get; init; }

    internal AttrId(string id)
    {
        Id = id;
    }
}

public sealed class AttrClasses : Attr
{
    public string[] Classes { get; init; }

    internal AttrClasses(string[] classes)
    {
        Classes = classes;
    }
}

public sealed class AttrOnClick : Attr
{
    public Action<MouseEventArgs> Action { get; init; }

    internal AttrOnClick(Action<MouseEventArgs> action)
    {
        Action = action;
    }
}

public sealed class AttrChildren : Attr
{
    public Element[] Children { get; init; }

    internal AttrChildren(Element[] children)
    {
        Children = children;
    }
}

public sealed class AttrHref : Attr
{
    public string Href { get; init; }

    internal AttrHref(string href)
    {
        Href = href;
    }
}

public sealed class AttrTitle : Attr
{
    public string Title { get; init; }

    internal AttrTitle(string title)
    {
        Title = title;
    }
}

public sealed class AttrAreaHidden : Attr
{
    public bool AriaHidden { get; init; }

    public AttrAreaHidden(bool ariaHidden)
    {
        AriaHidden = ariaHidden;
    }
}

public sealed class AttrTarget : Attr
{
    public string Target { get; init; }
    
    public AttrTarget(string target)
    {
        Target = target;
    }
}

public sealed class AttrOnChange : Attr
{
    public Action<ChangeEventArgs> Action { get; init; }

    internal AttrOnChange(Action<ChangeEventArgs> action)
    {
        Action = action;
    }
}

public sealed class AttrType : Attr
{
    public string Type { get; init; }

    internal AttrType(string type)
    {
        Type = type;
    }
}

public abstract class Attr
{
    public static AttrContent content(string? content) 
        => new(content);

    public static AttrId id(string id) 
        => new(id);
    
    public static AttrClasses classes(params string[] classes) 
        => new(classes);

    public static AttrOnClick onclick(Action<MouseEventArgs> action) 
        => new(action);
    
    public static AttrChildren children(params Element[] children) 
        => new(children);

    public static AttrHref href(string href)
        => new(href);

    public static AttrTitle title(string title)
        => new(title);

    public static AttrAreaHidden areaHidden(bool areaHidden)
        => new(areaHidden);

    public static AttrTarget target(string target)
        => new(target);

    public static AttrOnChange onchange(Action<ChangeEventArgs> action)
        => new(action);

    public static AttrType type(string type)
        => new(type);
}

