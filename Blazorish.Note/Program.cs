using Blazorish.Note.Data;
using Blazorish.Note.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddDbContext<NoteDbContext>(
    o => o.UseSqlite(@"Data Source=..\database.db")
);

builder.Services.AddTransient<Repository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();