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
    
    public record Model(Note[] Notes, Note NewNote);

    public abstract record Msg
    {
        public sealed record AddNote(Note Note) : Msg;
        public sealed record TryRefreshNoteList : Msg;
        public sealed record RefreshNoteList(Note[] Notes) : Msg;
        public sealed record UpdateName(string Name) : Msg;
        public sealed record UpdateContent(string Content) : Msg;
    }

    protected override (Model, Cmd<Msg>) Init() =>
        (new Model(Array.Empty<Note>(), new Note("", "", DateTime.Now)), 
            Cmd<Msg>.OfTaskPerform(Mediator.Send(new GetNotesQuery()), notes => new Msg.RefreshNoteList(notes)));


    protected override (Model, Cmd<Msg>) Update(Model model, Msg msg) => msg switch
    {
        Msg.AddNote {Note: var note} => 
            (model, Cmd<Msg>.OfTaskPerform(
                Mediator.Send(new AddNoteCommand(note with {Date = DateTime.Now})), _ => new Msg.TryRefreshNoteList())),
        Msg.TryRefreshNoteList => 
            (model, Cmd<Msg>.OfTaskPerform(
                Mediator.Send(new GetNotesQuery()), notes => new Msg.RefreshNoteList(notes))),
        Msg.RefreshNoteList {Notes: var notes} => 
            (model with {Notes = notes}, Cmd<Msg>.None),
        Msg.UpdateName {Name: var name} => 
            (model with {NewNote = model.NewNote with {Name = name}}, Cmd<Msg>.None),
        Msg.UpdateContent {Content: var content} => 
            (model with {NewNote = model.NewNote with {Content = content}}, Cmd<Msg>.None)
    };

    private Element AddNoteView(Model model) =>
        form(
            children(
                label(
                    content("Name: ")
                ),
                input(
                    onchange(e => Dispatch(new Msg.UpdateName(e.Value?.ToString() ?? "")))
                ),
                label(
                    content("Content: ")
                ),
                input(
                    onchange(e => Dispatch(new Msg.UpdateContent(e.Value?.ToString() ?? "")))
                ),
                btn(
                    type("button"),
                    content("Add note"),
                    onclick(_ => Dispatch(new Msg.AddNote(model.NewNote)))
                )
            )
        );
    
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
                AddNoteView(model),
                div(
                    children(
                        model.Notes.Select(NoteView).ToArray()
                    )
                )
            )
        );
}