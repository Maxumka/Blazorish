using Blazorish.Samples.Notes.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blazorish.Samples.Notes.Persistence;

public class NoteContext : DbContext
{
    public DbSet<NoteEntity> Notes => Set<NoteEntity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("NoteDb");
        base.OnConfiguring(optionsBuilder);
    }
}