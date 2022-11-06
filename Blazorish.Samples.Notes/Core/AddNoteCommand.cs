using Blazorish.Samples.Notes.Persistence;
using Blazorish.Samples.Notes.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazorish.Samples.Notes.Core;

public record AddNoteCommand(Note Note) : IRequest<int>
{
    public class CommandHandler : IRequestHandler<AddNoteCommand, int>
    {
        private readonly IDbContextFactory<NoteContext> _contextFactory;

        public CommandHandler(IDbContextFactory<NoteContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Task<int> Handle(AddNoteCommand request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            var noteEntity = new NoteEntity
            {
                Name = request.Note.Name,
                Content = request.Note.Content,
                Date = request.Note.Date
            };

            context.Notes.Add(noteEntity);

            return context.SaveChangesAsync(cancellationToken);
        }
    }
}