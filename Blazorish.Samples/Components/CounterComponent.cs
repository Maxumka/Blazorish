using Blazorish.Html;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Samples.Components;

[Route("/counter")]
public class CounterComponent : BlazorishSimpleComponent<CounterComponent.Model, CounterComponent.Msg>
{
    public record Model(int Count);

    public abstract record Msg
    {
        public sealed record Increment : Msg;
        public sealed record Decrement : Msg;
        public sealed record Reset : Msg;
    }

    protected override Model Init() => new(0);

    protected override Model Update(Model model, Msg msg) => msg switch
    {
        Msg.Increment => model with {Count = model.Count + 1},
        Msg.Decrement => model with {Count = model.Count - 1},
        _ => model with {Count = 0}
    };

    protected override Tag View(Model model) =>
        div(
            children(
                h1(
                    content("Counter")
                ),
                p(
                    content($"Current count: {model.Count}")
                ),
                btn(
                    content("Increment"),
                    classes("btn btn-primary"),
                    onclick(_ => Dispatch(new Msg.Increment()))
                ),
                btn(
                    content("Decrement"),
                    classes("btn btn-primary"),
                    onclick(_ => Dispatch(new Msg.Decrement()))
                ),
                btn(
                    content("Reset"),
                    classes("btn btn-primary"),
                    onclick(_ => Dispatch(new Msg.Reset()))
                )
            )
        );
}