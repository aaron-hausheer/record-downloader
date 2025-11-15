using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RecordDownloader.Web;
using RecordDownloader.Web.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

HttpClient apiClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5252")
};

builder.Services.AddScoped(sp => apiClient);
builder.Services.AddScoped<IRecordApiClient, RecordApiClient>();

await builder.Build().RunAsync();