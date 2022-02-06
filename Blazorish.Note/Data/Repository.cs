using Blazorish.Note.DataAccess;

namespace Blazorish.Note.Data;

public class Repository
{
    private readonly NoteDbContext _context;

    public Repository(NoteDbContext context)
        => _context = context;

    public IEnumerable<Note> GetAllNotes()
    {
        var notes = _context.NoteEntities
            .Select(n => new Note(n.Id, n.Title, n.Content))
            .ToArray();
        
        return notes;
    }
    
    public async Task<Note> AddNoteAsync(Note note)
    {
        var entity = new NoteEntity()
        {
            Title = note.Title,
            Content = note.Content
        };
        
        _context.NoteEntities.Add(entity);

        await _context.SaveChangesAsync();

        return note with {Id = entity.Id};
    }

    public async Task UpdateNoteAsync(Note note)
    {
        var entity = _context.NoteEntities
            .FirstOrDefault(n => n.Id == note.Id);

        if (entity is null)
        {
            return;
        }

        entity.Title = note.Title;
        entity.Content = note.Content;

        await _context.SaveChangesAsync();
    }
}