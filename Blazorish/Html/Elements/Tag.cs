using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish.Html.Elements;

public sealed class Tag : Element
{
    private readonly string _name;

    private readonly Attr[] _attributes;

    public Element[] Children =>
        _attributes.OfType<AttrChildren>().SelectMany(a => a.Children).ToArray();
    
    private Tag(string name, Attr[] attributes)
    {
        _name = name;
        _attributes = attributes;
    }
    
    internal override void Build(RenderTreeBuilder builder, ref int seq, object receiver)
    {
        builder.OpenElement(seq++, _name);
        
        foreach (var attr in _attributes.Where(a => a is AttrClasses))
        {
            var classes = (attr as AttrClasses)!.Classes;

            foreach (var @class in classes)
            {
                builder.AddAttribute(seq++, "class", @class);
            }
        }

        foreach (var attr in _attributes.Where(a => a is AttrOnClick))
        {
            var action = (attr as AttrOnClick)!.Action;
            builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(receiver, action));
        }
        
        foreach (var attr in _attributes.Where(a => a is AttrOnChange))
        {
            var action = (attr as AttrOnChange)!.Action;
            builder.AddAttribute(seq++, "onchange", EventCallback.Factory.Create(receiver, action));
        }
        
        foreach (var attr in _attributes.Where(a => a is AttrId))
        {
            var id = (attr as AttrId)!.Id;
            builder.AddAttribute(seq++, "id", id);
        }

        foreach (var attr in _attributes.Where(a => a is AttrHref))
        {
            var href = (attr as AttrHref)!.Href;
            builder.AddAttribute(seq++, "href", href);
        }
        
        foreach (var attr in _attributes.Where(a => a is AttrTitle))
        {
            var title = (attr as AttrTitle)!.Title;
            builder.AddAttribute(seq++, "title", title);
        }
        
        foreach (var attr in _attributes.Where(a => a is AttrAreaHidden))
        {
            var areaHidden = (attr as AttrAreaHidden)!.AriaHidden;
            builder.AddAttribute(seq++, "area-hidden", areaHidden);
        }
        
        foreach (var attr in _attributes.Where(a => a is AttrTarget))
        {
            var target = (attr as AttrTarget)!.Target;
            builder.AddAttribute(seq++, "target", target);
        }
        
        foreach (var attr in _attributes.Where(a => a is AttrType))
        {
            var type = (attr as AttrType)!.Type;
            builder.AddAttribute(seq++, "type", type);
        }
        
        foreach (var attr in _attributes)
        {
            if (attr is AttrContent {Content: var content})
            {
                builder.AddContent(seq++, content);
            }   
        }
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

    public static Tag a(params Attr[] attrs)
        => new("a", attrs);

    public static Tag span(params Attr[] attrs)
        => new("span", attrs);
    
    public static Tag nav(params Attr[] attrs)
        => new("nav", attrs);

    public static Tag main(params Attr[] attrs)
        => new("main", attrs);

    public static Tag article(params Attr[] attrs)
        => new("article", attrs);

    public static Tag input(params Attr[] attrs)
        => new("input", attrs);
    
    public static Tag label(params Attr[] attrs)
        => new("label", attrs);

    public static Tag form(params Attr[] attrs)
        => new("form", attrs);
}