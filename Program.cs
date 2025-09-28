using Microsoft.EntityFrameworkCore;
using Todo.Models;
using Todo.Services;

var builder = WebApplication.CreateBuilder(args);

// Register in memory db
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

// Controller
builder.Services.AddControllers();

// Service
builder.Services.AddScoped<ITodoService, TodoService>();

// TODO: temporary workaround to allow CORS
// ✅ Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin() // Allow all domains
            .AllowAnyMethod() // Allow GET, POST, PUT, DELETE, etc.
            .AllowAnyHeader(); // Allow any headers
    });
});

// Build
var app = builder.Build();

// ✅ Enable CORS middleware
app.UseCors();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Todos.Any())
    {
        context.Todos.AddRange(
            new Todo.Models.Todo
            {
                Id = 1,
                Title = "Book your scan",
                Description = "Scan your body for potential cancer and 500+ conditions in up to 13 organs.",
                CreatedDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(1),
                Status = TodoStatus.Active
            },
            new Todo.Models.Todo
            {
                Id = 2,
                Title = "5 minute questionnaire",
                Description = "Fill out a 5-minute form of your medical summary.",
                CreatedDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(1),
                Status = TodoStatus.Completed
            },
            new Todo.Models.Todo
            {
                Id = 3,
                Title = "Advanced MRI",
                Description = "An MRI that scans 13 organs - from your head to your pelvis.",
                CreatedDate = DateTime.UtcNow.AddDays(1),
                Status = TodoStatus.Active
            },
            new Todo.Models.Todo
            {
                Id = 4,
                Title = "Get an Ezra report",
                Description = "Your images are reviewed by a board-certified radiologist and an Ezra Medical Provider to create your Ezra Report within 10 days.",
                CreatedDate = DateTime.UtcNow.AddDays(-1),
                DueDate = DateTime.UtcNow.AddDays(-5),
                Status = TodoStatus.Active
            },
            new Todo.Models.Todo
            {
                Id = 5,
                Title = "Download the app",
                CreatedDate = DateTime.UtcNow.AddDays(-2),
                DueDate = DateTime.UtcNow.AddDays(-5),
                Status = TodoStatus.Completed
            }
        );
        context.SaveChanges();
    }
}

// 2️⃣ Configure middleware
app.MapControllers();

app.Run();