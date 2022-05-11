global using BISA.Server.Entities;
global using BISA.Shared.DTO;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using System.Text;
using BISA.Server.Data.DbContexts;
using BISA.Server.Services.AuthService;
using BISA.Server.Services.BookService;
using BISA.Server.Services.EbookService;
using BISA.Server.Services.EventService;
using BISA.Server.Services.ItemService;
using BISA.Server.Services.LibrisService;
using BISA.Server.Services.LoanService;
using BISA.Server.Services.MovieService;
using BISA.Server.Services.SearchService;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILibrisService, LibrisService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IEbookService, EbookService>();

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddHttpClient();

// Swagger with Bearer token
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("BisaBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BisaBearerAuth" }
            }, new List<string>() }
    });
});

// BisaDb
builder.Services.AddDbContext<BisaDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("BisaDbConnection")));

//UserDb
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("UserConnection")
    ));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();

// UserDb options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
});

// JWT
var jwtSettings = builder.Configuration.GetSection("JWTSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretForKey"]))
    };
});

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

// Seed database with LIBRIS
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var librisService = services.GetRequiredService<ILibrisService>();
    await librisService.SeedDatabase();
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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

