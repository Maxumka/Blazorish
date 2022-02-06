using Microsoft.EntityFrameworkCore;

namespace Blazorish.Note.DataAccess;

public class NoteEntity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
}

public class NoteDbContext : DbContext
{
    public DbSet<NoteEntity> NoteEntities { get; set; }
    
    public NoteDbContext(DbContextOptions options) : base(options) 
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NoteEntity>().ToTable("Note");
        modelBuilder.Entity<NoteEntity>().HasKey(n => n.Id);
        modelBuilder.Entity<NoteEntity>().Property(n => n.Title);
        modelBuilder.Entity<NoteEntity>().Property(n => n.Content);
    }
}