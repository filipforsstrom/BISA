global using Microsoft.EntityFrameworkCore;
using BISA.Server.Data.DbContexts;
using BISA.Server.Entities;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// BisaDb
builder.Services.AddDbContext<BisaDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("BisaDbConnection")));

//UserDb
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("UserConnection")
    ));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();

var app = builder.Build();

//// Deletes, creates and updates database anew with the seeded data
using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetService<BisaDbContext>())
    {
        context.Database.EnsureDeleted();
        //context.Database.Migrate();
        context.Database.EnsureCreated();
    }
    using (var context = scope.ServiceProvider.GetService<UserDbContext>())
    {
        context.Database.EnsureDeleted();
        //context.Database.Migrate();
        context.Database.EnsureCreated();
    }

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();