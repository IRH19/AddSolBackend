using AddSolBackend; // Import your files
using Microsoft.EntityFrameworkCore; // Import the SQL tools

var builder = WebApplication.CreateBuilder(args);

// --- 1. DATABASE CONNECTION ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// --- 2. ENABLE CORS (Frontend Connection) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// --- 3. MIDDLEWARE PIPELINE ---
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();

// --- 4. WEATHER DATA LOGIC (Keeping this for now until DB is ready) ---
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapControllers();
app.Run();

// --- 5. MODELS ---
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
