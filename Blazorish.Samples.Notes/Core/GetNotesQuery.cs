using Blazorish.Samples.Notes.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blazorish.Samples.Notes.Core;

public class GetNotesQuery : IRequest<Note[]>
{
    public class QueryHandler : IRequestHandler<GetNotesQuery, Note[]>
    {
        private readonly IDbContextFactory<NoteContext> _contextFactory;

        public QueryHandler(IDbContextFactory<NoteContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Task<Note[]> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            return context.Notes
                .Select(n => new Note(n.Name, n.Content, n.Date))
                .ToArrayAsync(cancellationToken);
        }
    }
}