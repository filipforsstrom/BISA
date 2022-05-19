global using BISA.Client.Services.BookService;
global using BISA.Client.Services.EbookService;
global using BISA.Client.Services.EventService;
global using BISA.Client.Services.ItemService;
global using BISA.Client.Services.LoanService;
global using BISA.Client.Services.MovieService;
global using BISA.Shared.ViewModels;
global using Blazored.LocalStorage;
global using System.Net.Http.Json;
using BISA.Client;
using BISA.Client.Services.AuthService;
using BISA.Client.Services.EventService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
