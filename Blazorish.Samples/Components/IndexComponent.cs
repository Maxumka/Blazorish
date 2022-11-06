using Blazorish.Html.Elements;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Samples.Components;

[Route("/")]
public class IndexComponent : BlazorishComponentOnlyHtml
{
    private static Element TitleView() =>
        h1(
            content("Hello, World")
        );
    
    protected override Element View() =>
        div(
            children(
                TitleView(),
                p(
                    content("Welcome to your new app.")
                )
            )
        );
}