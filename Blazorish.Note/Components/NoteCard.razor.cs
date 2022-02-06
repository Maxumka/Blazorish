using Microsoft.AspNetCore.Components;

namespace Blazorish.Note.Components;

public record NoteCardModel(Data.Note Note, bool IsExpand);

public abstract record NoteCardMsg
{
    public sealed record UpdateTitle(string Title) : NoteCardMsg;

    public sealed record UpdateContent(string Content) : NoteCardMsg;

    public sealed record ChangeExpand : NoteCardMsg;
}

public class NoteCardBase : BlazorProgram<NoteCardModel, NoteCardMsg>
{
    [Parameter]
    public NoteCardModel NoteCardModel { get; set; }

    protected override (NoteCardModel, Cmd<NoteCardMsg>) Init()
        => (NoteCardModel, Cmd<NoteCardMsg>.None());

    private (NoteCardModel, Cmd<NoteCardMsg>) UpdateTitle(NoteCardModel model, string title)
    {
        var note = model.Note with {Title = title};

        var updateModel = model with {Note = note};

        return (updateModel, Cmd<NoteCardMsg>.None());
    }
    
    private (NoteCardModel, Cmd<NoteCardMsg>) UpdateContent(NoteCardModel model, string content)
    {
        var note = model.Note with {Content = content};

        var updateModel = model with {Note = note};

        return (updateModel, Cmd<NoteCardMsg>.None());
    }
    
    protected override (NoteCardModel, Cmd<NoteCardMsg>) Update(NoteCardModel model, NoteCardMsg msg)
        => msg switch
        {
            NoteCardMsg.UpdateTitle (Title: var title)
                => UpdateTitle(model, title),
            NoteCardMsg.UpdateContent (Content: var content)
                => UpdateContent(model, content),
            NoteCardMsg.ChangeExpand
                => (model with {IsExpand = !model.IsExpand}, Cmd<NoteCardMsg>.None())
        };
}