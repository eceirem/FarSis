using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FarSis.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configure the database connection
builder.Services.AddDbContext<FarSisContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FarSisContext")
    ?? throw new InvalidOperationException("Connection string 'FarSisContext' not found.")));

// 🔹 Add MVC controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔹 Configure the HTTP request pipeline
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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
