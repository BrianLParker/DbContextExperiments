using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DbContextExperiments.Client;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>(selector: "#app");
        builder.RootComponents.Add<HeadOutlet>(selector: "head::after");
        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(uriString: "https://localhost:7276/")
        });
        await builder.Build().RunAsync();
    }
}
