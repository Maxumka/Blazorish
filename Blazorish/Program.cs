namespace Blazorish;

public abstract class Program<TModel, TMsg> : BlazorishProgram<TModel, TMsg>
    where TMsg : class
{
    protected abstract TModel Init();
    
    protected override void OnInitialized()
    {
        Model = Init();
    }
}

public abstract class ProgramAsync<TModel, TMsg> : BlazorishProgram<TModel, TMsg>
    where TMsg : class
{
    protected abstract Task<TModel> InitAsync();

    protected override async Task OnInitializedAsync()
    {
        Model = await InitAsync();
    }
}

