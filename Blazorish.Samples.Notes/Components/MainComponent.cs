using Blazorish.Cmd;
using Blazorish.Html.Elements;
using Blazorish.Samples.Notes.Core;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Blazorish.Samples.Notes.Components;

[Route("/")]
public class MainComponent : BlazorishComponent<MainComponent.Model, MainComponent.Msg>
{
    [Inject] private IMediator Mediator { get; set; }
    
    public record Model(Note[] Notes);

    public abstract record Msg
    {
        public sealed record AddNote(Note Note) : Msg;
        public sealed record TryRefreshNoteList : Msg;
        public sealed record RefreshNoteList(Note[] Notes) : Msg;
    }

    protected override (Model, Cmd<Msg>) Init() =>
        (new Model(Array.Empty<Note>()), 
            Cmd<Msg>.OfTaskPerform(Mediator.Send(new GetNotesQuery()), notes => new Msg.RefreshNoteList(notes)));


    protected override (Model, Cmd<Msg>) Update(Model model, Msg msg) => msg switch
    {
        Msg.AddNote {Note: var note} => 
            (model, Cmd<Msg>.OfTaskPerform(Mediator.Send(new AddNoteCommand(note)), _ => new Msg.TryRefreshNoteList())),
        Msg.TryRefreshNoteList => 
            (model, Cmd<Msg>.OfTaskPerform(Mediator.Send(new GetNotesQuery()), notes => new Msg.RefreshNoteList(notes))),
        Msg.RefreshNoteList {Notes: var notes} => 
            (model with {Notes = notes}, Cmd<Msg>.None)
    };

    private static Element NoteView(Note note) =>
        div(
            children(
                p(
                    content($"Note name: {note.Name}")
                ),
                p(
                    content(note.Content)
                ),
                p(
                    content($"Created date: {note.Date:h:mm:ss tt zz}")
                )
            )
        );

    protected override Element View(Model model) =>
        main(
            children(
                h1(
                    content("Notes")
                ),
                div(
                    children(
                        model.Notes.Select(NoteView).ToArray()
                    )
                ),
                btn(
                    content("Add note"),
                    onclick(_ => Dispatch(new Msg.AddNote(new Note(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.UtcNow))))
                )
            )
        );
}