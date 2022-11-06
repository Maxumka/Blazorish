using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazorish.Html.Elements;

public class Fragment : Element
{
    private readonly RenderFragment _renderFragment;

    internal Fragment(RenderFragment renderFragment)
    {
        _renderFragment = renderFragment;
    }

    internal override void Build(RenderTreeBuilder builder, ref int seq, object receiver)
    {
        builder.AddContent(seq++, _renderFragment);
    }

    public static Fragment create(RenderFragment fragment)
        => new(fragment);
}