namespace Blazorish.Html;

public sealed class Tag
{
    internal string Name { get; init; } 
    
    internal Attr[] Attrs { get; init; }

    internal Tag(string name, Attr[] attrs)
    {
        Name = name;
        Attrs = attrs;
    }
    
    public static Tag div(params Attr[] attrs)
        => new("div", attrs);

    public static Tag p(params Attr[] attrs)
        => new("p", attrs);

    public static Tag btn(params Attr[] attrs)
        => new("button", attrs);

    public static Tag ul(params Attr[] attrs)
        => new("ul", attrs);

    public static Tag li(params Attr[] attrs)
        => new("li", attrs);

    public static Tag h1(params Attr[] attrs)
        => new("h1", attrs);

    public static Tag em(params Attr[] attrs)
        => new("em", attrs);
    
    public static Tag table(params Attr[] attrs)
        => new("table", attrs);
    
    public static Tag thead(params Attr[] attrs)
        => new("thead", attrs);

    public static Tag tbody(params Attr[] attrs)
        => new("tbody", attrs);
    
    public static Tag tr(params Attr[] attrs)
        => new("tr", attrs);
    
    public static Tag th(params Attr[] attrs)
        => new("th", attrs);
    
    public static Tag td(params Attr[] attrs)
        => new("td", attrs);
}