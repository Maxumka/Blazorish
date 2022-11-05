# Blazorish
Naive implementation MVU in Blazor inspired by elmish (without razor syntax). <br />
I want to write applications in C# and Blazor based on model-view-update. If you too, try this. 
# Warning
Its not production ready, its not ready for anything. I just making this in free time. <br /> 
Just for fun.
# Commands 
The following commands are currently ready:
* None
* OfMsg
* OfFuncEither
* OfFuncPerform
* OfTaskEither
* OfTaskPerform
# Usage
Just create class and inherintce BlazorishSimpleComponent, in generic parameters add your model and msg.
Write your model, like this
```csharp
public record Model(int Count);
```
And add your messages 
```csharp

public abstract record Msg
{
    public sealed record Increment : Msg;
    public sealed record Decrement : Msg;
    public sealed record Reset : Msg;
}
```
After that implement abstract methods: Update, Init and View. 
```csharp
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
```
Also add route attribute to your component
```csharp
[Route("/counter")]
public class CounterComponent : BlazorishSimpleComponent<CounterComponent.Model, CounterComponent.Msg>
```
Optional, add static using if you don't want write static class for dsl
```csharp
global using static Blazorish.Html.Tag;
global using static Blazorish.Html.Attr;
```
Congratulations! You create blazor component with MVU architecture.

Blazorish.Samples project has examples based on defaut blazor WASM template.
