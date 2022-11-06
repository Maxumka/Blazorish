using Blazorish.Html.Elements;
using Microsoft.AspNetCore.Components.Routing;

namespace Blazorish.Samples.Shared;

public class BlazorishNavMenu : BlazorishComponentOnlyHtml
{
    private static Element NavLinkView(string spanClass, string href, string name) =>
        div(
            classes("nav-item px-3"),
            children(
                Component<NavLink>.create(
                    ("class", "nav-link"),
                    ("href", href),
                    ("ChildContent", 
                        div(
                            children(
                                span(
                                    classes(spanClass),
                                    areaHidden(true),
                                    content(name)
                                )
                        )))
                )
            )
        );
    
    protected override Element View() => 
        div(
            classes("sidebar"),
            children(
                div(
                    classes("top-row ps-3 navbar navbar-dark"),
                    children(
                        div(
                            classes("container-fluid"),
                            children(
                                a(
                                    classes("navbar-brand"),
                                    href(""),
                                    content("Blazorish.Samples")
                                )
                            )
                        )
                    )
                ),
                nav(
                    classes("flex-column"),
                    children(
                        NavLinkView("oi oi-home", "", "Home"),
                        NavLinkView("oi oi-plus", "counter", "Counter"),
                        NavLinkView("oi oi-list-rich", "fetchdata", "Fetch data")
                    )
                )
            )
        );
}