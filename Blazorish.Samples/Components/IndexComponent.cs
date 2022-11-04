using Blazorish.Html;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Samples.Components;

[Route("/")]
public class IndexComponent : BlazorishComponentOnlyHtml
{
    private static Tag TitleView() =>
        h1(
            content("Hello, World")
        );
    
    protected override Tag View() =>
        div(
            children(
                TitleView(),
                p(
                    content("Welcome to your new app.")
                )
            )
        );
}