using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudBlazorGitHubPages;
using MudBlazorGitHubPages.Util;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => http);

var appSettings = await http.GetFromJsonAsync<AppSettings>("appsettings.json", new System.Text.Json.JsonSerializerOptions() { Converters = { new UnknownEnumConverter() } });
builder.Services.AddSingleton(appSettings ?? new());

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorage();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.BrowserConsole()
    .CreateLogger();

await builder.Build().RunAsync();
