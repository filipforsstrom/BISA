global using Microsoft.AspNetCore.Components.Authorization;
global using BISA.Client.Services.BookService;
global using BISA.Client.Services.EventService;
global using BISA.Client.Services.ItemService;
global using BISA.Client.Services.AuthService;
global using BISA.Shared.ViewModels;
global using Blazored.LocalStorage;
global using System.Net.Http.Json;
using BISA.Client;

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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
