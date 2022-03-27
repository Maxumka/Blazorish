using Blazorish.Template.Data;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Template.Pages;

public record FetchDataModel(WeatherForecast[] Forecasts);

public abstract record FetchDataMsg
{
    public sealed record TryGetData : FetchDataMsg;
    public sealed record GetData(WeatherForecast[] Forecasts) : FetchDataMsg;
    public sealed record StopGettingData : FetchDataMsg;
}

public class FetchDataBase : BlazorProgram<FetchDataModel, FetchDataMsg>
{
    [Inject] private WeatherForecastService ForecastService { get; set; } 

    protected override (FetchDataModel, Cmd<FetchDataMsg>) Init()
    {
        var cmd = Cmd<FetchDataMsg>.OfAsyncPerform(
            func: ForecastService.GetForecastAsync,
            arg: DateTime.Now,
            msg: x => new FetchDataMsg.GetData(x)
        );

        var model = new FetchDataModel(Forecasts: Array.Empty<WeatherForecast>());

        return (model, cmd);
    }

    private (FetchDataModel, Cmd<FetchDataMsg>) UpdateTryGetData(FetchDataModel model)
    {
        var cmd = Cmd<FetchDataMsg>.OfAsyncPerform(
            func: ForecastService.GetForecastAsync,
            arg: DateTime.Now,
            msg: x => new FetchDataMsg.GetData(x)
        );

        return (model, cmd);
    }

    private (FetchDataModel, Cmd<FetchDataMsg>) UpdateGetData(FetchDataModel model, WeatherForecast[] forecasts)
    {
        var newModel = model with {Forecasts = forecasts};

        var cmd = Cmd<FetchDataMsg>.OfMsg(new FetchDataMsg.TryGetData());

        return (newModel, cmd);
    }

    private (FetchDataModel, Cmd<FetchDataMsg>) UpdateStopGettingData(FetchDataModel model)
    {
        return (model, Cmd<FetchDataMsg>.None());
    }

    protected override (FetchDataModel, Cmd<FetchDataMsg>) Update(FetchDataModel model, FetchDataMsg msg)
        => msg switch
        {
            FetchDataMsg.TryGetData 
                => UpdateTryGetData(model),
            FetchDataMsg.GetData (var v)
                => UpdateGetData(model, v),
            FetchDataMsg.StopGettingData
                => UpdateStopGettingData(model)
        };
}