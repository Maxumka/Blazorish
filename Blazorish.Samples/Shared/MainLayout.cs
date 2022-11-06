using Blazorish.Html.Elements;

namespace Blazorish.Samples.Shared;

public class MainLayout : BlazorishLayoutComponent
{
    protected override Element View() => 
        div(
            classes("page"),
            children(
                Component<BlazorishNavMenu>.create(),
                main(
                    children(
                        div(
                            classes("top-row px-4"),
                            children(
                                a(
                                    href("https://docs.microsoft.com/aspnet/"),
                                    target("_blank"),
                                    content("About")
                                )
                            )
                        ),
                        article(
                            classes("content px-4"),
                            children(
                                Fragment.create(Body)
                            )
                        )
                    )
                )
            )
        );
}