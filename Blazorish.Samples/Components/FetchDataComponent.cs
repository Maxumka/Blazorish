using Blazorish.Cmd;
using Blazorish.Html;
using Blazorish.Samples.Data;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Samples.Components;

[Route("/fetchdata")]
public class FetchDataComponent : BlazorishComponent<FetchDataComponent.Model, FetchDataComponent.Msg>
{
    [Inject] private WeatherForecastService ForecastService { get; set; }

    public record Model(WeatherForecast[] Forecasts);

    public abstract record Msg
    {
        public sealed record TryGetData : Msg;

        public sealed record GetData(WeatherForecast[] Forecasts) : Msg;
    }

    protected override (Model, Cmd<Msg>) Init()
    {
        var model = new Model(Array.Empty<WeatherForecast>());
        
        var cmd = Cmd<Msg>.OfTaskPerform(
            ForecastService.GetForecastAsync(DateTime.Now),
            x => new Msg.GetData(x)
        );

        return (model, cmd);
    }

    private (Model, Cmd<Msg>) UpdateTryGetData(Model model)
    {
        var cmd = Cmd<Msg>.OfTaskPerform(
            ForecastService.GetForecastAsync(DateTime.Now),
            x => new Msg.GetData(x)
        );

        return (model, cmd);
    }
    
    protected override (Model, Cmd<Msg>) Update(Model model, Msg msg) => msg switch
    {
        Msg.TryGetData 
            => UpdateTryGetData(model),
        Msg.GetData {Forecasts: var forecasts} 
            => (model with {Forecasts = forecasts}, Cmd<Msg>.None)
    };

    private static Tag LoadingView() =>
        p(
            children(
                em(
                    content("Loading...")
                )
            )
        );

    private Tag ViewWithData(Model model) =>
        table(
            classes("table"),
            children(
                thead(
                    children(
                        tr(
                            children(
                                th(
                                    content("Date")
                                ),
                                th(
                                    content("Temp. (C)")
                                ),
                                th(
                                    content("Temp. (F)")
                                ),
                                th(
                                    content("Summary")
                                )
                            )
                        )
                    )
                ),
                tbody(
                    children(
                        model.Forecasts.Select(f => 
                            tr(
                                children(
                                    td(
                                        content(f.Date.ToShortDateString())
                                    ),
                                    td(
                                        content(f.TemperatureC.ToString())
                                    ),
                                    td(
                                        content(f.TemperatureF.ToString())
                                    ),
                                    td(
                                        content(f.Summary)
                                    )
                                )
                            )).ToArray()
                    )
                )
            )
        );
    
    protected override Tag View(Model model) =>
        div(
            children(
                h1(
                    content("Weather forecast")
                ),
                p(
                    content("This component demonstrates fetching data from the server.")
                ),
                (!model.Forecasts.Any()
                ? LoadingView()
                : ViewWithData(model)),
                btn(
                    content("Refresh data"),
                    classes("btn btn-primary"),
                    onclick(_ => Dispatch(new Msg.TryGetData()))
                )
            )
        );
}