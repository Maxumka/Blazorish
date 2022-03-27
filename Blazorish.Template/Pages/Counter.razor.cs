using Blazorish.Cmd;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Template.Pages;

public record CounterModel(int Count);

public abstract record CounterMsg
{
    public sealed record Increment : CounterMsg;

    public sealed record Decrement : CounterMsg;

    public sealed record Reset : CounterMsg;
}

public class CounterBase : BlazorishProgram<CounterModel, CounterMsg>
{
    protected override (CounterModel, Cmd<CounterMsg>) Init()
        => (new(0), Cmd<CounterMsg>.None());

    protected override (CounterModel, Cmd<CounterMsg>) Update(CounterModel counterModel, CounterMsg counterMsg)
        => counterMsg switch
        {
            CounterMsg.Increment 
                => (counterModel with {Count = counterModel.Count + 1}, Cmd<CounterMsg>.None()),
            CounterMsg.Decrement 
                => (counterModel with {Count = counterModel.Count - 1}, Cmd<CounterMsg>.None()),
            CounterMsg.Reset 
                => (counterModel with {Count = 0}, Cmd<CounterMsg>.None())
        };
}