namespace Blazorish.Samples.Notes.Core;

public record Note(
    string Name, 
    string Content, 
    DateTime Date
);