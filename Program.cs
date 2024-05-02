using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;
using Wordle.NET.Models;
using Wordle.NET.WASM;
using Wordle.NET.WASM.Utility;
[assembly: AssemblyVersion("1.0.*")]
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<NotifierService>();
builder.Services.AddSingleton<TimerService>();
builder.Services.AddTransient<FileUtils>();

await builder.Build().RunAsync();