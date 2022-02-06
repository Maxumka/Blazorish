# Blazorish
Naive implementation MVU in Blazor inspired by elmish with hello world examples. <br />
I want to write applications in C# and Blazor based on model-view-update. If you too, try this. 
# Warning
Its not production ready, its not ready for anything. I just making this in free time. <br /> 
Proof of concepts.
# Usage
Create cs file for your pages and, add using Blazorish. 
Write your model, like this
```csharp
public record CounterModel(int Count);
```
And add your messages 
```csharp

public abstract record CounterMsg
{
    public sealed record Increment : CounterMsg;

    public sealed record Decrement : CounterMsg;

    public sealed record Reset : CounterMsg;
}
```
Create base component for your razor pages. 
Base component must be inherited from BlazorProgram<Model, Msg>, and implement abstract methods: Update, Init. 
```csharp
public class CounterBase : BlazorishComponentBase<CounterModel, CounterMsg>
{
    protected override CounterModel Init()
        => new(0);

    protected override (CounterModel, Cmd<CounterMsg>) Update(CounterModel counterModel, CounterMsg counterMsg)
        => counterMsg switch
        {
            CounterMsg.Increment => (counterModel with {Count = counterModel.Count + 1}, Cmd<CounterMsg>.None()),
            CounterMsg.Decrement => (counterModel with {Count = counterModel.Count - 1}, Cmd<CounterMsg>.None()),
            CounterMsg.Reset => (counterModel with {Count = 0}, Cmd<CounterMsg>.None())
        };
}
```
After that, inherit your razor page or component from CounterBase. Use property naming Model for getting current state pages. 
Use Dispatch method for update current state, pass message  
```razor
@page "/counter"

@inherits CounterBase

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @Model.Count</p>

<button class="btn btn-primary" @onclick="() => Dispatch(new CounterMsg.Increment())">
    Increment
</button>

<button class="btn btn-primary" @onclick="() => Dispatch(new CounterMsg.Decrement())">
    Decrement
</button>

<button class="btn btn-primary" @onclick="() => Dispatch(new CounterMsg.Reset())">
    Reset
</button>
```
Congratulations! You create razor page with MVU architecture.

In Blazorish.Template has examples based on defaut blazor WASM template.
