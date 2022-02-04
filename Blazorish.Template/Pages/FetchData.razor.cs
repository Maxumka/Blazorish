using Blazorish.Template.Data;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Template.Pages;

public record FetchDataModel(WeatherForecast[] Forecasts);

public abstract record FetchDataMsg
{
    public sealed record TryGetData() : FetchDataMsg;
    public sealed record GetData(WeatherForecast[] Forecasts) : FetchDataMsg;
    public sealed record ClearData() : FetchDataMsg;
}

public class FetchDataBase : ProgramAsync<FetchDataModel, FetchDataMsg>
{
    [Inject]
    private WeatherForecastService ForecastService { get; set; }

    protected override async Task<FetchDataModel> InitAsync()
    {
        var forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        
        return new FetchDataModel(forecasts);
    }

    private (FetchDataModel, Cmd<FetchDataMsg>) UpdateTryGetData(FetchDataModel model)
    {
        var cmd = Cmd<FetchDataMsg>.OfAsync(
            func: ForecastService.GetForecastAsync,
            arg: DateTime.Now.AddDays(1),
            msg: x => new FetchDataMsg.GetData(x)
        );

        return (model, cmd);
    }

    private (FetchDataModel, Cmd<FetchDataMsg>) UpdateGetData(FetchDataModel model, WeatherForecast[] forecasts)
    {
        var newModel = model with {Forecasts = forecasts};

        return (newModel, Cmd<FetchDataMsg>.None());
    }

    private (FetchDataModel, Cmd<FetchDataMsg>) UpdateClearData(FetchDataModel model)
    {
        var newModel = model with {Forecasts = Array.Empty<WeatherForecast>()};

        return (newModel, Cmd<FetchDataMsg>.None());
    }

    protected override (FetchDataModel, Cmd<FetchDataMsg>) Update(FetchDataModel model, FetchDataMsg msg)
        => msg switch
        {
            FetchDataMsg.TryGetData 
                => UpdateTryGetData(model),
            FetchDataMsg.GetData {Forecasts: var v}
                => UpdateGetData(model, v),
            FetchDataMsg.ClearData
                => UpdateClearData(model)
        };
}