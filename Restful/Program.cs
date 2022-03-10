var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PersonDbContext>(
    options => options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //Swagger nur im DevelopmentModus on
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/person", async (PersonDbContext db) => { return await db.Persons.ToListAsync(); })
    .Produces<Person>();

app.MapGet("/person/{id}", async (int id, PersonDbContext db) =>
{
    var persons = await db.Persons.FindAsync(id);
    if (persons == null)
        return Results.NotFound();
    return Results.Ok(persons);
})
    .Produces<Person>()
    .Produces(404);

app.MapPost("/person", async (Person persons, PersonDbContext db) =>
{
    await db.Persons.AddAsync(persons);
    await db.SaveChangesAsync();
    return Results.Created($"/person/{persons.Id}", persons);
})
    .Produces<Person>(201);

app.MapPut("/person/{id}", async (int id, Person persons, PersonDbContext db) =>
{
    if (id != persons.Id)
        return Results.BadRequest();
    if (!await db.Persons.AnyAsync(x => x.Id == id))
        return Results.NotFound();

    db.Update(persons);
    await db.SaveChangesAsync();
    return Results.Ok(persons);
}
    )
    .Produces<Person>()
    .Produces(400);

app.MapDelete("/person/{id}", async (int id, PersonDbContext db) =>
{
    var persons = await db.Persons.FindAsync(id);
    if (persons is null)
        return Results.NotFound();

    db.Persons.Remove(persons);
    await db.SaveChangesAsync();

    return Results.NoContent();
}
    )
    .Produces(204)
    .Produces(404);

app.Run();