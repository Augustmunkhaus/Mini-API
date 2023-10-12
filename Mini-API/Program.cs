using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using Data;
using Service;
using Model;

var builder = WebApplication.CreateBuilder(args);

// S�tter CORS s� API'en kan bruges fra andre dom�ner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilf�j DbContext factory som service.
builder.Services.AddDbContext<ThreadPostContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilf�j DataService s� den kan bruges i endpoints
builder.Services.AddScoped<DataService>();


var app = builder.Build();

// Seed data hvis n�dvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data p�, hvis databasen er tom. Ellers ikke.
}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Middlware der k�rer f�r hver request. S�tter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});


// DataService f�s via "Dependency Injection" (DI)
app.MapGet("/", (DataService service) =>
{
    return new { message = "Hello World!" };
});


app.MapGet("api/threadposts", (DataService service) =>
{
    return service.GetThreadPosts();



});

app.MapGet("api/threadposts/{id}", (DataService service, int id) =>
{
    return service.GetThreadPost(id);
});

app.MapGet("api/threadposts/users/{id}", (DataService service, int id) =>
{
    return service.GetUser(id);
});

app.MapGet("api/threadposts/comments", (DataService service) =>
{
    return service.GetComments();
});

app.MapPost("api/threadposts", (DataService service, ThreadPost threadpost) =>
{ 
    service.CreateThreadPost(threadpost);

return Results.Created($"api/threadposts", threadpost);

});
app.MapPost("api/comments", (DataService service, Comment comment) =>
{
    service.CreateComment(comment);

    return Results.Created($"api/comments/{comment.Id}", comment);

});

app.MapPut("api/threadposts/{id}", (int Id) =>
{
    var thread = GetThreadById(id);


});

app.Run();

