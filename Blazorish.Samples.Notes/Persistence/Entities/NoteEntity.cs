namespace Blazorish.Samples.Notes.Persistence.Entities;

public class NoteEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Content { get; set; }
    
    public DateTime Date { get; set; }
}