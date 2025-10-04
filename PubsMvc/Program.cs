using Microsoft.EntityFrameworkCore;
using PubsMvc.Models;
var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------
// Register application services
// -----------------------------------------------------

// Add MVC controllers + views
builder.Services.AddControllersWithViews();

// Register your repositories
builder.Services.AddScoped<IRepository<Title>, TitleRepository>();
builder.Services.AddScoped<IRepository<Author>, AuthorRepository>();
builder.Services.AddScoped<IRepository<Publisher>, PublisherRepository>();

// Register DbContext with connection string from appsettings.json
builder.Services.AddDbContext<pubsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -----------------------------------------------------
// Build the app
// -----------------------------------------------------
var app = builder.Build();

// -----------------------------------------------------
// Configure the HTTP request pipeline
// -----------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // default 30 days
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();