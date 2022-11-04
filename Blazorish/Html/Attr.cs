using Microsoft.AspNetCore.Components.Web;

namespace Blazorish.Html;

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
    
    public static AttrChildren children(params Tag[] children) 
        => new(children);
}

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
    public Tag[] Children { get; init; }

    internal AttrChildren(Tag[] children)
    {
        Children = children;
    }
}