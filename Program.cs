using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FarSis.Data;
using FarSis.Models;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext and Identity services
builder.Services.AddDbContext<FarSisContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FarSisContext")
    ?? throw new InvalidOperationException("Connection string 'FarSisContext' not found.")));

// Add Identity services
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<FarSisContext>()
    .AddDefaultTokenProviders();

// Add controllers and views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Apply migrations to the database at startup and seed the database (only in development)
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var context = services.GetRequiredService<FarSisContext>();
        var logger = services.GetRequiredService<ILogger<SeedData>>();

        try
        {
            // Apply any pending migrations
            await context.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied successfully.");

            // Call the SeedData method to seed the database
            await SeedData.Initialize(services, userManager, roleManager, logger);
            logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during migration or seeding
            logger.LogError(ex, "An error occurred while applying migrations or seeding the database.");
        }
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🔹 Updated routing approach (replaces UseEndpoints)
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);*/

app.UseEndpoints(endpoints =>
{
        endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
});
app.Run();
