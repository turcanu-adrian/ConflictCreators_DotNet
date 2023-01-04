using Infrastructure.Repositories;
using MediatR;
using WebAPI.Hubs;
using Microsoft.EntityFrameworkCore;
using Application.Abstract;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ConflictCreators;Trusted_Connection=True"));
builder.Services.AddSingleton<IGameManager, GameManager>();
builder.Services.AddScoped<IPromptRepository, PromptRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(typeof(IPromptRepository));
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(
    config => config.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000")
            .AllowCredentials()
            );

app.UseAuthorization();

app.MapControllers();
app.MapHub<GameHub>("/gameHub");
app.Run();

Console.WriteLine("APP STARTED");