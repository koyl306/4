using Microsoft.EntityFrameworkCore;
using pract9;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("LabDb"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/experiments", async (AppDbContext db) =>
    await db.LabExp.ToListAsync());

app.MapGet("/experiments/{id}", async (int id, AppDbContext db) =>
    await db.LabExp.FindAsync(id)
        is Experiment experiment
            ? Results.Ok(experiment)
            : Results.NotFound());

app.MapPost("/experiments", async (Experiment experiment, AppDbContext db) =>
{
    db.LabExp.Add(experiment);
    await db.SaveChangesAsync();

    return Results.Created(
        $"/experiments/{experiment.Id}",
        experiment);
});

app.MapPut("/experiments/{id}", async (
    int id,
    Experiment inputExperiment,
    AppDbContext db) =>
{
    var experiment = await db.LabExp.FindAsync(id);

    if (experiment is null)
        return Results.NotFound();

    experiment.Name = inputExperiment.Name;
    experiment.Date = inputExperiment.Date;
    experiment.UsedMaterial = inputExperiment.UsedMaterial;
    experiment.Result = inputExperiment.Result;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/experiments/{id}", async (int id, AppDbContext db) =>
{
    if (await db.LabExp.FindAsync(id)
        is Experiment experiment)
    {
        db.LabExp.Remove(experiment);

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    return Results.NotFound();
});

app.UseHttpsRedirection();

app.Run();