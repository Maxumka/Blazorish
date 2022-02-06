using Blazorish.Note.Data;
using Blazorish.Note.Components;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Note.Pages;

public record IndexModel(IEnumerable<NoteCardModel> NoteCardModels);

public abstract record IndexMsg
{
    public sealed record TryAddNote : IndexMsg;
    
    public sealed record AddNote(Data.Note Note) : IndexMsg;
    
    public sealed record TryUpdateNote(NoteCardModel NoteCardModel) : IndexMsg;
}

public class IndexBase : BlazorProgram<IndexModel, IndexMsg>
{
    [Inject]
    public Repository Repository { get; set; }
    
    protected override (IndexModel, Cmd<IndexMsg>) Init()
    {
        var noteCardModels = Repository.GetAllNotes()
            .Select(n => new NoteCardModel(n, false));

        var indexModel = new IndexModel(noteCardModels);

        return (indexModel, Cmd<IndexMsg>.None());
    }

    private (IndexModel, Cmd<IndexMsg>) TryAddNote(IndexModel model)
    {
        var note = new Data.Note(0, "New note", "");

        var cmd = Cmd<IndexMsg>.OfAsyncResult(
            func: Repository.AddNoteAsync,
            arg: note,
            msg: n => new IndexMsg.AddNote(n)
        );

        return (model, cmd);
    }

    private (IndexModel, Cmd<IndexMsg>) AddNote(IndexModel model, Data.Note note)
    {
        var noteCardModel = new NoteCardModel(note, false);

        var noteCardModels = model.NoteCardModels
            .Append(noteCardModel);

        var updateModel = model with {NoteCardModels = noteCardModels};

        return (updateModel, Cmd<IndexMsg>.None());
    }

    private (IndexModel, Cmd<IndexMsg>) TryUpdateNote(IndexModel model, NoteCardModel noteCardModel)
    {
        var noteCardModels = model.NoteCardModels
            .Select(n => n.Note.Id == noteCardModel.Note.Id ? noteCardModel : n);

        var updateModel = model with {NoteCardModels = noteCardModels};

        var cmd = Cmd<IndexMsg>.OfAsync(
            func: Repository.UpdateNoteAsync,
            arg: noteCardModel.Note
        );

        return (updateModel, cmd);
    }

    protected override (IndexModel, Cmd<IndexMsg>) Update(IndexModel model, IndexMsg msg)
        => msg switch
        {
            IndexMsg.TryAddNote 
                => TryAddNote(model),
            IndexMsg.AddNote (Note: var note)
                => AddNote(model, note),
            IndexMsg.TryUpdateNote (NoteCardModel: var noteCardModel)
                => TryUpdateNote(model, noteCardModel)
        };
}