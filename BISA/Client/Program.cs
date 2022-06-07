global using BISA.Client.Services.AuthService;
global using BISA.Client.Services.BookService;
global using BISA.Client.Services.EbookService;
global using BISA.Client.Services.EventService;
global using BISA.Client.Services.ItemService;
global using BISA.Client.Services.LoanService;
global using BISA.Client.Services.MovieService;
global using BISA.Shared.ViewModels;
global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
global using System.Net.Http.Json;
using Append.Blazor.Printing;
using BISA.Client;
using BISA.Client.Services.AuthService;
using BISA.Client.Services.EventService;
using BISA.Client.Services.ReservationsService;
using BISA.Client.Services.ReservationsService;
using BISA.Client.Services.SearchService;
using BISA.Client.Services.StatisticsService;
using BISA.Client.Services.UserRoleService;
using BISA.Client.Services.UserService;
using BISA.Client.Services.UserService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using BISA.Client.Services.UserService;
using BISA.Client.Services.ReservationsService;
using BISA.Client.Services.InventoryService;
using BISA.Client.Services.SessionService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IEbookService, EbookService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationsService, ReservationsService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IPrintingService, PrintingService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
